
using HandsFree.Application.Core.DependencyInjection;
using HandsFree.Domain.Core.DependencyInjection;
using HandsFree.Domain.Core.Utils;
using HandsFree.Service.Core.Authorization;
using HandsFree.Service.Core.DependencyInjection;
using HandsFree.Service.Core.Mvc.ModelBinding;
using HandsFree.Service.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.WebEncoders;
using Microsoft.OpenApi.Models;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace HandsFreeWebServer.Presentation.Shared;

public partial class Startup
{
    public static IWebHostEnvironment? AppEnvironment { get; private set; }

    public IConfiguration Configuration { get; }

    public Startup(IWebHostEnvironment env, IConfiguration configuration)
    {
        AppEnvironment = env;
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<WebEncoderOptions>(options =>
        {
            options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAny", builder =>
        {

            builder.WithOrigins("*", "*", "*")
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
        });
        });
        services.AddRazorPages()
      .ConfigureApplicationPartManager(this.ConfigureApplicationParts)
                .AddMvcOptions(options =>
                {
                    options.Filters.AddHandsFreeServiceFilters(services);
                    // 認証必須設定
                    options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireIlexAuthenticatedUser().Build()));
                    // モデルに対するValueObjectのマッピング処理
                    options.ModelBinderProviders.Insert(0, new HandsFreeServiceModelBinderProvider());
                    if (!string.IsNullOrEmpty(this.Configuration.GetSection("MvcOptions:MaxModelBindingCollectionSize").Value))
                    {
                        options.MaxModelBindingCollectionSize = this.Configuration.GetValue<int>("MvcOptions:MaxModelBindingCollectionSize");
                    }
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.AddJsonConverters();
                })
                .AddSessionStateTempDataProvider();

        services.AddHttpContextAccessor();

        //services.AddSingleton<HtmlEncoder,W>

        // DI（依赖注入）的批量注册设置
        var assemblies = StartupCore.GetAppAssemblies(true, "").OrderBy(_ => _, new AssemblyLayerComparer());
        services.AddHandsFreeService(assemblies);

        services.AddHandsFreeHostedService();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HandsFreeService API", Version = "v1" });
        });
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(TimeoutActionFilter));
        })
         .ConfigureApiBehaviorOptions(options =>
        {
            // 在绑定到 Model 时，自动返回 400 错误的行为被阻止。
            options.SuppressModelStateInvalidFilter = true;
        })
        .AddJsonOptions(options =>
         {
             options.JsonSerializerOptions.AddJsonConverters();
         });

        // Session定义
        {
            var section = this.Configuration.GetSection("Session");
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(this.GetRequiredSectionValue<double>(section, "IdleTimeout"));
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                /*
                 * 2: Strict: 不向其他域传递 cookie
                 * 1: Lax: 仅在对其他域的 GET 请求时传递 cookie
                 * 0: None: 对其他域的任何方法都可以传递 cookie
                 */
                options.Cookie.SameSite = (SameSiteMode)this.GetRequiredSectionValue<int>(section, "Cookie.SameSite");
                options.Cookie.Name = this.GetRequiredSectionStringValue(section, "Cookie.Name");
                options.Cookie.Path = this.GetRequiredSectionStringValue(section, "Cookie.Path");
                options.Cookie.Domain = section.GetValue<string?>("Cookie.Domain");
            });

            // Redis 个别设置的滑动过期设置（符合Session定义）
            services.AddTransient<DistributedCacheEntryOptions>(_ => new() { SlidingExpiration = TimeSpan.FromMinutes(this.GetRequiredSectionValue<double>(section, "IdleTimeout")) });
        }

        services.AddIlexHttpClient();

    }

    private void ConfigureApplicationParts(ApplicationPartManager apm)
    {
        // 插件先加载，通常子系统界面后加载。先加载的会优先处理
        foreach (var assembly in StartupCore.GetAppAssemblies(false, "Web"))
        {
            var name = assembly.GetName().Name;
            if (apm.ApplicationParts.Any(_ => _.Name == name))
            {
                // 如果已经存在注册的 DLL，则忽略
                continue;
            }
            apm.ApplicationParts.Add(new CompiledRazorAssemblyPart(assembly));
            apm.ApplicationParts.Add(new AssemblyPart(assembly));
        }
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
    {
        app.ApplicationServices.SetActivator();
        // 配置其他中间件和设置
        // 使用 CORS 中间件
        // 根据需要添加其他服务
        app.UseSession();
        app.UseCors("AllowAny");
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HandsFreeService API V1");
            });
        }
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
        //app.UseHttpsRedirection();
        app.UseStatusCodePages();
        app.UseStaticFiles(new StaticFileOptions()
        {

        });
        //app.UseAuthentication();
        //app.UseAuthorization();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapRazorPages();
        });

        app.Use(async (context, next) =>
        {
            if (!env.IsDevelopment())
            {
                context.Response.GetTypedHeaders().CacheControl =
                new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                {
                    NoStore = true,
                    NoCache = true,
                };
            }
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("Content-Security-Policy", "frame-src 'self'");
            context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            await next();
        });
        this.ConfigureBuilder(app, env, applicationLifetime);

    }
    partial void ConfigureBuilder(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime);

    private T GetRequiredSectionValue<T>(IConfigurationSection section, string key)
    where T : struct
    {
        var value = section.GetValue<Nullable<T>>(key);
        return (T)Guard.GetNotNull(value, $"设定值{ConfigurationPath.Combine(section.Path, key)}");
    }
    private string GetRequiredSectionStringValue(IConfigurationSection section, string key)
    {
        var value = section.GetValue<string?>(key);
        return Guard.GetNotNull(value, $"设定值{ConfigurationPath.Combine(section.Path, key)}");
    }
}

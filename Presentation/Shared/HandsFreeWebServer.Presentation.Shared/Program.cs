using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration.Json;
using HandsFree.Infrastructure.Core.Configuration;
using HandsFree.Domain.Core.Data;

namespace HandsFreeWebServer.Presentation.Shared;

public partial class Program
{
  public static void Main(string[] args)
  {
    AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

    using (var startupHost = CreateHostBuilder(args).Build())
    {
      using (var scope = startupHost.Services.CreateScope())
      {
        var factory = scope.ServiceProvider.GetRequiredService<IHandsFreeDbContextFactory<IHandsFreeDbContext>>();
        IHost host;
        using (var dbContext = factory.CreateDbContext())
        {
          host = CreateHostBuilder(args, dbContext).Build();
          using (var innerScope = host.Services.CreateScope())
          {

          }
        }
        ApplicationStarting(host);
        host.Run();
      }
    }
  }
  static partial void ApplicationStarting(IHost host);
  public static IHostBuilder CreateHostBuilder(string[] args, IHandsFreeDbContext dbContext) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                 webBuilder
                      .ConfigureAppConfiguration((context, configurationBuilder) =>
                       {
                         //configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                         InsertJsonConfiguration(configurationBuilder);
                         configurationBuilder
                              .AddHandsFreeConfiguration(dbContext);
                       })
                      .ConfigureKestrel((context, options) =>
                       {
                           // 排除server头部
                           options.AddServerHeader = false;
                         ConfigureKestrel(context, options);
                       })
                      .ConfigureLogging(logBuilder =>
                       {
                         logBuilder
                              .ClearProviders()
                              .AddConsole()
                              .AddDebug();
                       })
                      .UseStartup<Startup>();
               });

  static partial void ConfigureKestrel(WebHostBuilderContext context, KestrelServerOptions options);

  private static void InsertJsonConfiguration(IConfigurationBuilder builder)
  {
    var conf = builder.Sources.Reverse().FirstOrDefault(_ => _.GetType() == typeof(JsonConfigurationSource));
    if (conf != null)
    {
      var tempBuilder = new ConfigurationBuilder();
      tempBuilder.AddJsonConfiguration();
      var index = builder.Sources.IndexOf(conf) + 1;
      foreach (var c in tempBuilder.Sources)
      {
        builder.Sources.Insert(index++, c);
      }
    }
    else
    {
      // 如果不存在其他的 JSON 文件读取配置，则可以直接添加。
      builder.AddJsonConfiguration();
    }
  }

  public static IHostBuilder CreateHostBuilder(string[] args) =>
     Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
     {
       webBuilder.ConfigureAppConfiguration((context, builder) =>
       {
         InsertJsonConfiguration(builder);
       }).UseStartup<StartupCore>(); 
     });
}

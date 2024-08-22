using HandsFree.Domain.Core.DependencyInjection;
using HandsFree.Domain.Core.ValueObjects;
using System.Reflection;

namespace HandsFreeWebServer.Presentation.Shared;

public class StartupCore
{
  public StartupCore()
  {

  }
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddMemoryCache();
    services.AddHttpContextAccessor();

    var assemblies = GetAppAssemblies(true, "");
    services.AddHandsFreeService(assemblies);

    services.AddTransient(typeof(ICurrentEntryPoint), _ => new DummyEntryPoint());
    services.AddTransient(typeof(ICurrentFunctionId), _ => new DummyFunctionId());

    services.AddHttpClient();
    }
  public void Configure()
  {
  }
  private class DummyEntryPoint : ICurrentEntryPoint
  {
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Type Get() => this.GetType();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool TryGet(out Type? current)
    {
      current = this.GetType();
      return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="current"></param>
    public void Set(Type current) => throw new NotImplementedException();
  }

    private class DummyFunctionId : ICurrentFunctionId
    {
        public void Set(FunctionId current) => throw new NotImplementedException();
        public bool TryGet(out FunctionId? value)
        {
            value = null;
            return false;
        }
        /// <summary>
        /// 设置一个虚拟值。
        /// </summary>
        /// <returns></returns>
        public FunctionId Get() => new FunctionId("XX1_X0000");
    }

    internal static IEnumerable<Assembly> GetAppAssemblies(bool distinct, params string[] targetFileParts)
  {
    var rootDir = AppDomain.CurrentDomain.BaseDirectory;
    var rootDi = new DirectoryInfo(rootDir);
    var usedAssemblies = new HashSet<string>();

    foreach (var filePart in targetFileParts)
    {
      var targetDll = $"HandsFree*{(string.IsNullOrEmpty(filePart) ? "*" : filePart)}*.dll";
      var assemblyFiles = Directory.GetFiles(rootDir, targetDll, SearchOption.TopDirectoryOnly);

      foreach (var assemblyFile in assemblyFiles.OrderBy( x => x.Contains("Core") ? -1 : !x.Contains("Web") ? 0 : x.Length))
      {
        var fi = new FileInfo(assemblyFile);

        var asm = AppDomain.CurrentDomain.GetAssemblies()
            .Where(_ => _.FullName != null)
             .Where(_ => _.FullName != null && _.FullName.StartsWith("HandsFree"))
            .Select(_ => new { Assembly = _, FileInfo = new FileInfo(_.Location) })
            .FirstOrDefault(_ => _.FileInfo.Name == fi.Name)?.Assembly ?? Assembly.LoadFrom(assemblyFile);
        if (usedAssemblies.Contains(fi.Name))
        {
          continue;
        }
        if (distinct)
        {
          usedAssemblies.Add(fi.Name);
        }
        yield return asm;
      }
    }
  }

}

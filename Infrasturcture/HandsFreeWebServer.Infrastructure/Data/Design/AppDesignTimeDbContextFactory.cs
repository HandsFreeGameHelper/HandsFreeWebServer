using HandsFree.Infrastructure.Core.Data.Design;
using System.ComponentModel;

namespace HandsFreeWebServer.Infrastructure.Data.Builders.Design;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[EditorBrowsable(EditorBrowsableState.Never)]
public class AppDesignTimeDbContextFactory : DesignTimeDbContextFactoryBase<HandsFreeServiceDbContext>
{
  public override HandsFreeServiceDbContext CreateDbContext(string[] args)
  {
    return new HandsFreeServiceDbContext(this.GetDesignTimeServiceProvider());
  }
}

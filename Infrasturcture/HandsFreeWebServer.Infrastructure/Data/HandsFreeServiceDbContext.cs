using HandsFreeWebServer.Domain.Data;
using HandsFree.Domain.Core.Data;
using HandsFreeWebServer.Infrastructure.Data.Builders.Compiled;
using Microsoft.EntityFrameworkCore;

namespace HandsFreeWebServer.Infrastructure.Data;

[ConnectionStringName("DefaultConnection")]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Runtime.CompilerServices.CompilerGenerated]
public partial class HandsFreeServiceDbContext : HandsFreeServiceDbContextBase, IHandsFreeServiceDbContext
{
  public HandsFreeServiceDbContext(IServiceProvider serviceProvider)
         : base(serviceProvider)
  {
  }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
    optionsBuilder.UseModel(GMDbContextModel.Instance);
  }
}

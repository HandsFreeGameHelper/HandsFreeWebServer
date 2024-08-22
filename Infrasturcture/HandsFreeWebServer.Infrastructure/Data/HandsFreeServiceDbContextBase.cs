using HandsFreeWebServer.Domain.Entities;
using HandsFree.Domain.Core.Utils;
using HandsFreeWebServer.Infrastructure.Data.Builders;
using HandsFree.Infrastructure.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace HandsFreeWebServer.Infrastructure.Data;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Runtime.CompilerServices.CompilerGenerated]
public abstract partial class HandsFreeServiceDbContextBase : HandsFreeDbContext
{
  public HandsFreeServiceDbContextBase(IServiceProvider serviceProvider)
          : base(serviceProvider)
  {
  }

  private DbSet<Password>? _password;
  //private DbSet<Log>? _log;

  public virtual DbSet<Password> Password { get => Guard.GetNotNull(this._password, nameof(this.Password)); set => this._password = value; }
  //public virtual DbSet<Log> Log { get => Guard.GetNotNull(this._log, nameof(this.Log)); set => this._log = value; }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.AddPassword();
    //modelBuilder.AddLog();

    base.OnModelCreating(modelBuilder);
  }
}

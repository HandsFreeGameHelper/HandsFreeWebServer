using HandsFreeWebServer.Domain.Entities;
using HandsFree.Domain.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace HandsFreeWebServer.Domain.Data;

[System.Runtime.CompilerServices.CompilerGenerated]
public partial interface IHandsFreeServiceDbContextBase : IHandsFreeDbContext
{
  DbSet<Password> Password { get; set; }
  //DbSet<Log> Log { get; set; }
}


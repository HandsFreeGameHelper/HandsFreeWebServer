using HandsFreeWebServer.Domain.Data;
using HandsFree.Domain.Core.Data;
using HandsFree.Service.Core.Mvc;

[assembly: SubDomain("HandsFreeWebService")]
[assembly: DbContextOnly(typeof(IHandsFreeServiceDbContext))]

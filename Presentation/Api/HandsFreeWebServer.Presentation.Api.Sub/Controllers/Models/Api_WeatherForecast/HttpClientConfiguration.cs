using HandsFree.Domain.Core.Services.HttpClients;
using Microsoft.Extensions.Configuration;

namespace HandsFreeWebServer.Presentation.Api.Controllers.Models.Api_WeatherForecast
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HttpClientConfiguration : HttpClientConfigurationBase
    {
        /// <summary>
        /// <see cref="HttpClientConfiguration"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="configuration"></param>
        public HttpClientConfiguration(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

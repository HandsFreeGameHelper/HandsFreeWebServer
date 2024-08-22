using HandsFree.Domain.Core.ValueObjects;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace HandsFreeWebServer.Presentation.Api.Controllers.Models.Api_WeatherForecast
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ResponseDataModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("currentFunctionId")]
        public FunctionId? CurrentFunctionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("weathers")]
        public List<Weather>? Weathers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("httpMethod")]
        public string? HttpMethod { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public class Weather 
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonPropertyName("date")]
            public DateTime Date { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonPropertyName("temperatureC")]
            public int TemperatureC { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonPropertyName("temperatureF")]
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

            /// <summary>
            /// 
            /// </summary>
            [JsonPropertyName("summary")]
            public string? Summary { get; set; }
        }
    }
}

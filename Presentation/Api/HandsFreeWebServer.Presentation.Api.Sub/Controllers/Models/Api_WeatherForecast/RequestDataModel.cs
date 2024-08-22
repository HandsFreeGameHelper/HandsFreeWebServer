using HandsFree.Domain.Core.DataAnnotations;
using HandsFree.Domain.Core.ValueObjects;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace HandsFreeWebServer.Presentation.Api.Controllers.Models.Api_WeatherForecast
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RequestDataModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(NeedsFullMessage = true)]
        [JsonPropertyName("currentFunctionId")]
        public FunctionId? CurrentFunctionId { get; set; }
    }
}

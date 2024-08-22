using HandsFree.Domain.Core.DependencyInjection;
using HandsFree.Domain.Core.Services.HttpClients.Models;
using HandsFree.Service.Core.Mvc;
using HandsFree.Service.Core.Mvc.Filters;
using HandsFree.Service.Core.Mvc.Models;
using HandsFreeWebServer.Presentation.Api.Controllers.Models.Api_WeatherForecast;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HandsFreeWebServer.Presentation.Api.Sub.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [WebApiKeyValidateIgnore]
    public class Api_WeatherForecast : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
         };

        private readonly ILogger<Api_WeatherForecast> _logger;
        private readonly ICurrentFunctionId _currentFunctionId;
        private readonly ICurrentRequestId _currentRequestId;

        /// <summary>
        /// /
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="currentFunctionId"></param>
        /// <param name="currentRequestId"></param>
        public Api_WeatherForecast(ILogger<Api_WeatherForecast> logger, ICurrentFunctionId currentFunctionId, ICurrentRequestId currentRequestId)
        {
            _logger = logger;
            _currentFunctionId = currentFunctionId;
            _currentRequestId = currentRequestId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [Route("api/v1/[controller]")]
        [HttpPost]
        public IActionResult OnPost(RequestObject<RequestMetadataModel, RequestDataModel> requestModel)
        {
            var response = new ResponseObject<ResponseMetadataModel, ResponseDataModel>()
            {
                Metadata = new()
                {
                    Status = new(System.Net.HttpStatusCode.OK),
                    Version = this.GetRouteVersion(),
                    Timestamp = this.GetCurrentDateTime(),
                    RequestId = requestModel.Metadata!.RequestId
                },
                Response = new ResponseDataModel()
                {
                    CurrentFunctionId = requestModel.Request?.CurrentFunctionId,
                    Weathers = Enumerable.Range(1, 5).Select(index => new ResponseDataModel.Weather
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    }).ToList(),
                    HttpMethod = this.Request.Method,
                }
            };

            return this.CreateResponseContent(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/v1/[controller]")]
        [HttpGet]
        public IActionResult OnGet([FromQuery] GetRequestModel request)
        {
            var requestModel = this.GetObject<RequestMetadataModel, RequestDataModel>(request);
            var response = new ResponseObject<ResponseMetadataModel, ResponseDataModel>()
            {
                Metadata = new()
                {
                    Status = new(System.Net.HttpStatusCode.OK),
                    Version = this.GetRouteVersion(),
                    Timestamp = this.GetCurrentDateTime(),
                    RequestId = requestModel.Metadata?.RequestId
                },
                Response = new ResponseDataModel()
                {
                    CurrentFunctionId = _currentFunctionId.Get(),
                    Weathers = Enumerable.Range(1, 5).Select(index => new ResponseDataModel.Weather
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    }).ToList(),
                    HttpMethod = this.Request.Method,
                }
            };

            return this.CreateResponseContent(response);
        }
    }
}
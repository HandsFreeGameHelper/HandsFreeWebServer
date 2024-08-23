using HandsFree.Domain.Core.Services.HttpClients;
using HandsFree.Domain.Core.Services.HttpClients.Models;
using HandsFree.Domain.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Text;

namespace HandsFreeWebServer.Presentation.Web.Sub.Areas.Main.Pages.Web_Index
{
  /// <summary>
  /// 
  /// </summary>
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;
    private IHandsFreeServiceHttpClientFactory _clientFactory { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [BindProperty]
    public IndexViewModel IndexViewModel { get; set; } = new IndexViewModel();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="handsFreeServiceHttpClientFactory"></param>
    public IndexModel(ILogger<IndexModel> logger, IHandsFreeServiceHttpClientFactory handsFreeServiceHttpClientFactory)
    {
      _logger = logger;
      _clientFactory = handsFreeServiceHttpClientFactory;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnGet()
    {
      var uuid = Guid.NewGuid();
      var weatherForecastRequestModel = new RequestObject<RequestMetadataModel, Api.Controllers.Models.Api_WeatherForecast.RequestDataModel>()
      {
        Metadata = new RequestMetadataModel
        {
          RequestId = new(uuid),
          System = new("HandsFreeService")
        },
        Request = new()
        {
          CurrentFunctionId = new("Web_Index"),
        },
      };
      var client = this._clientFactory.CreateClient(new("Api_WeatherForecast"), "v1");
      var content = new JsonStringContent(weatherForecastRequestModel, Encoding.UTF8, "application/json");
      var response = client.PostAsync(string.Empty, content).Result;
      var response2 = client.GetAsync($"?d={Uri.EscapeDataString(weatherForecastRequestModel.ToJsonString())}").Result;

      try
      {
        response.EnsureSuccessStatusCode();
        var result = response.Content.ToModel<ResponseObject<ResponseMetadataModel, Api.Controllers.Models.Api_WeatherForecast.ResponseDataModel>>();
        var result2 = response2.Content.ToModel<ResponseObject<ResponseMetadataModel, Api.Controllers.Models.Api_WeatherForecast.ResponseDataModel>>();
        IndexViewModel.ResponseDataModel.Add(result.Response!);
        IndexViewModel.ResponseDataModel.Add(result2.Response!);
        Console.WriteLine(result.Response?.CurrentFunctionId?.ToString());
        Console.WriteLine(result.Response?.Weathers?.ToJsonString());
        Console.WriteLine(result2.Response?.CurrentFunctionId?.ToString());
        Console.WriteLine(result2.Response?.Weathers?.ToJsonString());
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
  }
}
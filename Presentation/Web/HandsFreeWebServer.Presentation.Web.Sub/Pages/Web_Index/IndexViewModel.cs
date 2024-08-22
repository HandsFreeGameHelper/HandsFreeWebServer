using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsFreeWebServer.Presentation.Web.Sub.Pages.Web_Index
{
    /// <summary>
    /// 
    /// </summary>
    public partial class IndexViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Api.Controllers.Models.Api_WeatherForecast.ResponseDataModel> ResponseDataModel { get; set; } = new();
        
    }
}

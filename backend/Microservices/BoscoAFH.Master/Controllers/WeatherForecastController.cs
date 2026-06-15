using BoscoAFH.CommonService;

namespace BoscoAFH.Master.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerModuleDoc.BoscoAFHMaster)]
    [Authorize]
    public class WeatherForecastController (IWeatherForecastService weatherForecast) : BaseController
    {
        /// <summary>
        /// Retrieves the weather forecast asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the weather forecast. <see cref="WeatherForecast"/></returns>
        /// <response code="200">Weather forecast found</response>
        /// <response code="204">No weather forecast found.</response>
        [HttpGet]
        [ActionName(CommonActionName.Common_API.getWeatherForecast)]
        [ProducesResponseType(StatusCodes.Status204NoContent)] 
        public async Task<IActionResult> GetAsync()
        {
            return ApiResultArgs(await weatherForecast.GetSummaries(), APIHttpType.HttpPost);
        }
    }
}
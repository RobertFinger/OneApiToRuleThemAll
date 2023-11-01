using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using OneStreamAssessment.Authentication;
using OneStreamAssessment.Models;

namespace OneStreamAssessment.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]  //access_as_user
    public class LakeAndWeatherController : ControllerBase
    {
        private readonly DataManager _data;
        private readonly ApiKeyAuthFilter _filter;
        private readonly ILogger<LakeAndWeatherController> _logger;

        public LakeAndWeatherController(DataManager data, ApiKeyAuthFilter filter, ILogger<LakeAndWeatherController> logger)
        {
            _data = data;
            _filter = filter;
            _logger = logger;
        }

        [HttpGet("/lake", Name = "getlake")]
        public async Task<List<LakeStatistics>> GetLakeData([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _data.GetDataFromLakeApiAsync();
        }

        [ApiKeyAuthFilter]
        [HttpPost("/lake", Name = "postlake")]
        public async Task<bool> PostLakeData([FromBody] LakeStatistics lake)
        {
            return await _data.PostDataToLakeApiAsync(lake);
        }


        [HttpGet("/weather", Name = "getweather")]
        public async Task<List<AirStatistics>> GetWeatherData([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _data.GetDataFromAirApiAsync();
        }

        [ApiKeyAuthFilter]
        [HttpPost("/weather", Name = "postweather")]
        public async Task<bool> PostWeatherData([FromBody] AirStatistics weather)
        {
            return await _data.PostDataToAirApiAsync(weather);
        }

        [HttpGet("getcombined", Name = "getcombined")]
        public async Task<List<CombinedData>> GetCombinedData([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var combinedList = new List<CombinedData>();
            var combined = new CombinedData();
            
            combined.WeatherData = await _data.GetDataFromAirApiAsync();
            combined.LakeData = await _data.GetDataFromLakeApiAsync();

            combinedList.Add(combined);
            return combinedList;
        }
    }
}
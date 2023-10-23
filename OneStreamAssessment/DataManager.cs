using Newtonsoft.Json;
using OneStreamAssessment.Models;
using System.Text;

namespace OneStreamAssessment
{
    public class DataManager
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DataManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<LakeStatistics>> GetDataFromLakeApiAsync()
        {
            using (var client = _httpClientFactory.CreateClient("LakeApi"))
            {
                var response = await client.GetAsync("/Lake");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lakeStats = JsonConvert.DeserializeObject<List<LakeStatistics>>(content);

                    return lakeStats ?? new List<LakeStatistics>();
                }
                else
                {
                    throw new Exception($"Failed to retrieve data from /Lake. Status code: {response.StatusCode}");
                }
            }
        }

        public async Task<bool> PostDataToLakeApiAsync(LakeStatistics lakeData)
        {
            using (var client = _httpClientFactory.CreateClient("LakeApi"))
            {
                var content = new StringContent(JsonConvert.SerializeObject(lakeData), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/lake", content);

                return response.IsSuccessStatusCode;

            }
        }

        public async Task<List<AirStatistics>> GetDataFromAirApiAsync()
        {
            using (var client = _httpClientFactory.CreateClient("AirApi"))
            {
                var response = await client.GetAsync("/Weather");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var airStats = JsonConvert.DeserializeObject<List<AirStatistics>>(content);

                    return airStats ?? new List<AirStatistics>();
                }
                else
                {
                    throw new Exception($"Failed to retrieve data from /weather. Status code: {response.StatusCode}");
                }
            }
        }

        public async Task<bool> PostDataToAirApiAsync(AirStatistics airData)
        {
            using (var client = _httpClientFactory.CreateClient("AirApi"))
            {
                var content = new StringContent(JsonConvert.SerializeObject(airData), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/weather", content);

                return response.IsSuccessStatusCode;
            }
        }
    }
}

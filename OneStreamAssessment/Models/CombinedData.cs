namespace OneStreamAssessment.Models
{
    public class CombinedData
    {
        public string Id { get; set; }  
        public List<LakeStatistics> LakeData { get; set; }
        public List<AirStatistics> WeatherData { get; set; }
    }
}

namespace OneStreamAssessment.Models
{
    public class AirStatistics
    {
        public int? Id { get; set; }
        public DateTime? Date { get; set; }
        public string? City { get; set; }
        public Weather? weather { get; set; }
        public int? Temperature { get; set; }
        public bool? Celcius { get; set; }
    }

    public enum Weather
    {
        Freezing,
        Bracing,
        Chilly,
        Cool,
        Mild,
        Warm,
        Balmy,
        Hot,
        Sweltering,
        Scorching
    }
}

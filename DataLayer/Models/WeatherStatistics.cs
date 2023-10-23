namespace DataLayer.Models
{
    public class WeatherStatistics
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }
        public string City { get; set; }
        public Weather weather { get; set; }
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

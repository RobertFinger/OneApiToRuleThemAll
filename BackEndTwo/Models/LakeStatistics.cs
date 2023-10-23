namespace LakeStat.Models
{
    public class LakeStatistics
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? Celcius { get; set; }
        public int? Temperature { get; set; }
        public DateTime? WeatherDate { get; set; }
        public Wave? Waves { get; set; }


    }

    public enum Wave
    {
        Calm,
        Choppy,
        Rough,
        Tidal
    }
}

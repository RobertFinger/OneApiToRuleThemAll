namespace DataLayer.Models
{
    public class LakeStatistics
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool Celcius { get;set; } 
        public int Temperature { get; set; }
        public DateTime DateTime { get; set; }
        public Wave Waves { get; set; }


    }

    public enum Wave
    {
        Calm,
        Choppy,
        Rough,
        Tidal
    }
}

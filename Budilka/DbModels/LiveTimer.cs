namespace Budilka.DbModels
{
    public class LiveTimer
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public SoundFile? Sound { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Budilka.DbModels
{
    public class TimerDbContext : DbContext
    {
        public TimerDbContext(DbContextOptions<TimerDbContext> options) : base(options)
        {
        }
        public TimerDbContext() : base() { }

        public virtual DbSet<SoundFile> Sounds { get; set; }
        public virtual DbSet<LiveTimer> Timers { get; set; }
    }
}

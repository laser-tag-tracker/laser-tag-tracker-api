using LaserTagTrackerApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LaserTagTrackerApi.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();

            modelBuilder.Entity<User>().HasMany(u => u.Matches).WithOne().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Match>().HasKey(m => m.Id);
            modelBuilder.Entity<Match>().Property(m => m.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Match>().Property(m => m.PlayerName);
            modelBuilder.Entity<Match>().Property(m => m.Rank);
            modelBuilder.Entity<Match>().Property(m => m.Score);
            modelBuilder.Entity<Match>().Property(m => m.Precision);
            modelBuilder.Entity<Match>().Property(m => m.TeamScore);
            modelBuilder.Entity<Match>().Property(m => m.Date);

            modelBuilder.Entity<Match>().Property(m => m.ChestGiven);
            modelBuilder.Entity<Match>().Property(m => m.BackGiven);
            modelBuilder.Entity<Match>().Property(m => m.ShouldersGiven);
            modelBuilder.Entity<Match>().Property(m => m.GunGiven);

            modelBuilder.Entity<Match>().Property(m => m.ChestReceived);
            modelBuilder.Entity<Match>().Property(m => m.BackReceived);
            modelBuilder.Entity<Match>().Property(m => m.ShouldersReceived);
            modelBuilder.Entity<Match>().Property(m => m.GunReceived);

            modelBuilder.Entity<Match>().Property(m => m.Address);
            modelBuilder.Entity<Match>().Property(m => m.Latitude);
            modelBuilder.Entity<Match>().Property(m => m.Longitude);
        }
    }
}
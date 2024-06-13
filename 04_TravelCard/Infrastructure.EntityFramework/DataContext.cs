
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EntityFramework
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    //public class DataContext : DbContext
    {
        //public DbSet<Manager> Managers { get; set; }

        public DbSet<Travel> Travels { get; set; }

        public DbSet<TravelPoint> TravelPoints { get; set; }

        public DbSet<User> Users { get; set; }

        //public DataContext()
        //{
        //    Database.EnsureCreated();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Связь 1 к n (Путешествие - компаньоны)
            modelBuilder.Entity<Travel>()
                .HasMany(t => t.Users)
                .WithMany(u => u.Travels);
            
            // Связь 1 к n (Путешествие - остановки)
            modelBuilder.Entity<Travel>()
                .HasMany(t => t.TravelPoints)
                .WithOne(tp => tp.Travel)
                .HasForeignKey (tp => tp.TravelId);
        
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
            //    optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=admin3725;Database=TravelCard_db");
            //    base.OnConfiguring(optionsBuilder);
            //     optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            //this.Database.EnsureCreated();
        //}
    }
}

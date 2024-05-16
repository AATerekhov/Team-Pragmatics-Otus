using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Infrastructure.EntityFramework
{
    /// <summary>
    /// Контекст.
    /// </summary>
    public class DatabaseContext:DbContext
    {
        /// <summary>
        /// Пользователи.
        /// </summary>
        public DbSet<User> Users { get; set; }
        public DbSet<PlaceType> PlaceTypes { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Fuelling> Fuellings { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options) { }        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasIndex(u => u.Logo).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<PlaceType>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<PlaceType>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<PlaceType>()
                .HasMany(t => t.Places)
                .WithOne(p => p.PlaceType)
                .HasForeignKey(p => p.PlaceTypeID);

            modelBuilder.Entity<Place>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Place>().HasIndex(u => u.Name).IsUnique();

            modelBuilder.Entity<Fuelling>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Fuelling>().HasIndex(u => u.Name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

    }


}

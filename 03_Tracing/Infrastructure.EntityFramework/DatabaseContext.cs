using Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<MapPoint> Points { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceType> PleceTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TODO: настроить FluentAPI для связей.
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

    }
}

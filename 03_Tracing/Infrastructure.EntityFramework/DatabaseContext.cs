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
        /// <summary>
        /// Пользователи.
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Гографисечкие точки.
        /// </summary>
        public DbSet<MapPoint> Points { get; set; }
        /// <summary>
        /// Места посещения.
        /// </summary>
        public DbSet<Place> Places { get; set; }
        /// <summary>
        /// Типы мест.
        /// </summary>
        public DbSet<PlaceType> PleceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TODO: настроить FluentAPI для связей.
            //Для User внешние ключи, пока, не определены.
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

    }
}

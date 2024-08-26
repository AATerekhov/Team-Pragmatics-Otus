﻿using Microsoft.EntityFrameworkCore;

namespace UserApi.Helpers
{
    public static class MigrationsManager
    {
        public static void MigrateDatabase<TDbContext>(this IHost host)
        where TDbContext: DbContext
        {
            var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
            context.Database.Migrate();
        }
    }
}
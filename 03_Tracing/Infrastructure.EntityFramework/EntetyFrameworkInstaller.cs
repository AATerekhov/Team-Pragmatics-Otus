using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;



namespace Infrastructure.EntityFramework
{
    public static class EntetyFrameworkInstaller
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services, string connectionString) 
        {
            services.AddDbContext<DatabaseContext>(optionBuilder => optionBuilder.UseNpgsql(connectionString));
            
            return services;
        }
    }
}

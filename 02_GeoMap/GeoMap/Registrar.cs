using AutoMapper;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using GeoMap.Settings;

namespace GeoMap
{
    /// <summary>
    /// Регистратор сервиса.
    /// </summary>
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {           
            var defaultConnection = configuration.Get<ApplicationSettings>();
            services.AddSingleton(defaultConnection);
            return services.AddSingleton((IConfigurationRoot)configuration)
                .InstallServices()
                .ConfigureContext(defaultConnection.ConnectionString)
                .InstallRepositories();
        }

        private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IUserService, UserService>();
            return serviceCollection;
        }

        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IUserRepository, UserRepository>();
            return serviceCollection;
        }
    }
}

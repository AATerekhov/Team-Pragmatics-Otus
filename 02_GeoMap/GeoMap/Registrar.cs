using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
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
                .AddScoped<IUserService, UserService>()
                .AddScoped<IPlaceTypeService, PlaceTypeService>()
                .AddScoped<IPlaceService, PlaceService> ()
                .AddScoped<IFuellingService, FuellingService>();
            return serviceCollection;
        }

        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IPlaceTypeRepository, PlaceTypeRepository>()
                .AddScoped<IPlaceRepository, PlaceRepository>()
                .AddScoped<IFuellingRepository, FuellingRepository>();
            return serviceCollection;
        }
    }
}

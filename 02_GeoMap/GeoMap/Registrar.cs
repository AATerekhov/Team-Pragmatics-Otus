using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using GeoMap.Settings;
using MassTransit;
using GeoMap.Consumer;

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

        /// <summary>
        /// Конфигурирование RMQ.
        /// </summary>
        /// <param name="configurator"> Конфигуратор RMQ. </param>
        /// <param name="configuration"> Конфигурация приложения. </param>
        public static void ConfigureRmq(IRabbitMqBusFactoryConfigurator configurator, IConfiguration configuration)
        {
            var rmqSettings = configuration.Get<ApplicationSettings>().RmqSettings;
            configurator.Host(rmqSettings.Host,
                rmqSettings.VHost,
                h =>
                {
                    h.Username(rmqSettings.Login);
                    h.Password(rmqSettings.Password);
                });
        }

        /// <summary>
        /// регистрация эндпоинтов
        /// </summary>
        /// <param name="configurator"></param>
        public static void RegisterEndPoints(IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.ReceiveEndpoint($"masstransit_event_create_user", e =>
            {
                e.Consumer<EventUserConsumer>();
                e.UseMessageRetry(r =>
                {
                    r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
                });
                e.PrefetchCount = 1;
                e.UseConcurrencyLimit(1);
            });

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

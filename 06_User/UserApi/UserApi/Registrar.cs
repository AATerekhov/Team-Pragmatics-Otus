using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserApi.Settings;
using UserApi.DataAccess.EntityFramework;

namespace UserApi
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var aplicationSettings = configuration.Get<ApplicationSettings>();
            services.AddSingleton(aplicationSettings);
            return services.AddSingleton((IConfigurationRoot)configuration)
                .ConfigureContext(aplicationSettings.ConnectionString);
        }
        public static IServiceCollection ConfigureContext(this IServiceCollection services,
         string connectionString)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(connectionString,
                    optionBuilder => optionBuilder.MigrationsAssembly("UserApi.DataAccess.EntityFramework"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            return services;
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
    }
}

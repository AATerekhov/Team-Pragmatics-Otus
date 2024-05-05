using Microsoft.AspNetCore.Cors.Infrastructure;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories;
using TracingProgmatica.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace TracingProgmatica
{
    /// <summary>
    /// Регистратор сервиса.
    /// </summary>
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationSettings = configuration.Get<ApplicationSettings>();
            services.AddSingleton(applicationSettings)
                    .AddSingleton((IConfigurationRoot)configuration)
                    .InstallServices()
                    .ConfigureContext(applicationSettings.ConnectionString)
                    .InstallRepositories();
            return services;
        }

        private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
            .AddTransient<ICourseService, CourseService>()
                .AddTransient<ILessonService, LessonService>();
            return serviceCollection;
        }

        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<ICourseRepository, CourseRepository>()
                .AddTransient<ILessonRepository, LessonRepository>();
            return serviceCollection;
        }
    }
}

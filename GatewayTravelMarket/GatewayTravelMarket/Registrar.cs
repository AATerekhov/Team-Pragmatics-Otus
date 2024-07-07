using GatewayTravelMarket.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace GatewayTravelMarket
{
    public static  class Registrar
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var apiGatewayConnection = configuration.Get<ApplicationSettings>().ApiGatewaySettings;
            services.AddSingleton(apiGatewayConnection);
            //TODO: Переделать получениея Settings.
            services.AddHttpClient("PlaseTypesServiceClient", c => c.BaseAddress = new Uri(apiGatewayConnection.GeoMapServiceBaseUrl));
            services.AddHttpClient("TravelServiceClient", c => c.BaseAddress = new Uri(apiGatewayConnection.TravelCardServiceBaseUrl));
            services.Configure<ApiGatewaySettings>(options =>
            {
                //var configuration = new ConfigurationBuilder()
                //            .SetBasePath(Directory.GetCurrentDirectory())
                //            .AddJsonFile("appsettings.json")
                //            .Build();
                //options.TravelCardServiceBaseUrl = configuration["ApiGatewaySettings:TravelCardServiceBaseUrl"];
                //options.GeoMapServiceBaseUrl = configuration["ApiGatewaySettings:GeoMapServiceBaseUrl"];
                //options.ValidApiKeys = configuration["ApiGatewaySettings:ValidApiKeys"];
                options.TravelCardServiceBaseUrl = apiGatewayConnection.TravelCardServiceBaseUrl;
                options.GeoMapServiceBaseUrl = apiGatewayConnection.GeoMapServiceBaseUrl;
                options.ValidApiKeys = apiGatewayConnection.ValidApiKeys;
            });
            return services;
        }
    }
}

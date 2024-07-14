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
            services.AddHttpClient("GeoMapServiceClient", c => c.BaseAddress = new Uri(apiGatewayConnection.GeoMapServiceBaseUrl));
            services.AddHttpClient("TravelServiceClient", c => c.BaseAddress = new Uri(apiGatewayConnection.TravelCardServiceBaseUrl));
            services.Configure<ApiGatewaySettings>(options =>
            {
                options.TravelCardServiceBaseUrl = apiGatewayConnection.TravelCardServiceBaseUrl;
                options.GeoMapServiceBaseUrl = apiGatewayConnection.GeoMapServiceBaseUrl;
                options.ValidApiKeys = apiGatewayConnection.ValidApiKeys;
            });
            return services;
        }
    }
}

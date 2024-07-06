using GatewayTravelMarket.Settings;

namespace GatewayTravelMarket
{
    public static  class Registrar
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: Переделать получениея Settings.
            services.AddHttpClient("ProductServiceClient", c => c.BaseAddress = new Uri("http://localhost:52199"));
            services.AddHttpClient("OrderServiceClient", c => c.BaseAddress = new Uri("http://localhost:52200"));            
            services.Configure<ApiGatewaySettings>(options =>
            {
                var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
                options.TravelCardServiceBaseUrl = configuration["ApiGatewaySettings:TravelCardServiceBaseUrl"];
                options.GeoMapServiceBaseUrl = configuration["ApiGatewaySettings:GeoMapServiceBaseUrl"];
                options.ValidApiKeys = configuration["ApiGatewaySettings:ValidApiKeys"];

            });
            return services;
        }
    }
}

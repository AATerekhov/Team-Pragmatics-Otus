
using AutoMapper;
using GeoMap.Mapping;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Repositories.Abstractions;

namespace GeoMap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Добавили настройки Mapping.
            //InstallAutomapper(builder.Services);
            builder.Services.AddServices(builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(typeof(IFuellingService));
            builder.Services.AddAutoMapper(typeof(IPlaceTypeService));
            builder.Services.AddAutoMapper(typeof(IPlaceService));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                db.Database.Migrate();
            }

            app.Run();
        }

        //Добавление настроек Mapping к HostService.
        private static IServiceCollection InstallAutomapper(IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));                 
            return services;
        }
        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMappingsProfile>();
                cfg.AddProfile<PlaceTypeMappingsProfile>();
                cfg.AddProfile<PlaceMappingsProfile>();
                cfg.AddProfile<FuellingMappingsProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.UserMappingsProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.PlaceTypeMappingsProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.PlaceMappingsProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.FuellingMappingsProfile>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}

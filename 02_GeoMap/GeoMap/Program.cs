
using AutoMapper;
using GeoMap.Mapping;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Implementations;
using Services.Implementations.Comsumers;
using Services.Repositories.Abstractions;

namespace GeoMap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMassTransit(x => {
                x.AddConsumer<EventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    Registrar.ConfigureRmq(cfg, builder.Configuration);
                    Registrar.RegisterEndPoints(cfg);
                });
            });

            builder.Services.AddHostedService<MasstransitService>();

            //Добавили настройки Mapping.
            //InstallAutomapper(builder.Services);
            builder.Services.AddServices(builder.Configuration);
            
            builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(typeof(FuellingService));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}

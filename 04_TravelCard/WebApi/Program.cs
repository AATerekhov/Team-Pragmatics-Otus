using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using MassTransit;
using WebApi.Mapping;
using Services.Implementations.Mapping;
using WebApi.Helper;
using System.Globalization;
using Newtonsoft.Json;
using WebApi.Settings;
using Services.Implementations.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//EntityFramework
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        optionsBuilder => optionsBuilder.MigrationsAssembly("Infrastructure.EntityFramework"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

//MassTransit
builder.Services.AddHostedService<MasstransitService>();
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var rmqSettings = builder.Configuration.Get<ApplicationSettings>().RmqSettings;
        cfg.Host(rmqSettings.Host,
            rmqSettings.VHost,
            h =>
            {
                h.Username(rmqSettings.Login);
                h.Password(rmqSettings.Password);
            });
        cfg.ReceiveEndpoint($"masstransit_event_create_user", e =>
        {
            e.Consumer<EventConsumer>();
            e.UseMessageRetry(r =>
            {
                r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            });
            e.PrefetchCount = 1;
            e.UseConcurrencyLimit(1);
        });
    });
});

//Policy For Resource Sharing
builder.Services.AddCors(option => option.AddDefaultPolicy(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

//Mapping
builder.Services.AddAutoMapper(typeof(UserMappingProfile));
builder.Services.AddAutoMapper(typeof(UserMappingProfileDto));
builder.Services.AddAutoMapper(typeof(TravelMappingProfile));
builder.Services.AddAutoMapper(typeof(TravelMappingProfileDto));
builder.Services.AddAutoMapper(typeof(TravelPointMappingProfile));
builder.Services.AddAutoMapper(typeof(TravelPointMappingProfileDto));

//IOC
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITravelService, TravelService>();
builder.Services.AddScoped<ITravelRepository, TravelRepository>();
builder.Services.AddScoped<ITravelPointService, TravelPointService>();
builder.Services.AddScoped<ITravelPointRepository, TravelPointRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.UseRouting();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.MigrateDatabase<DataContext>();

app.Run();

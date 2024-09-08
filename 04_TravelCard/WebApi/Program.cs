using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using Infrastructure.Repositories.Implementations;
using MassTransit;
using WebApi.Mapping;
using Services.Implementations.Mapping;
using WebApi.Helper;
using WebApi.Settings;
using WebApi.Consumers;

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
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<EventUserConsumer>();
    configurator.UsingRabbitMq((context, configurator) =>
    {
        var rmqSettings = builder.Configuration.Get<ApplicationSettings>()!.RmqSettings;
        configurator.Host(rmqSettings.Host,
                    rmqSettings.VHost,
                    h =>
                    {
                        h.Username(rmqSettings.Login);
                        h.Password(rmqSettings.Password);
                    });
        configurator.ConfigureEndpoints(context);
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

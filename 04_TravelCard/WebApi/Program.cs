using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using System;
using WebApi.Mapping;
using Services.Implementations.Mapping;
using WebApi.Helper;
using System.Globalization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddControllers().AddNewtonsoftJson(options =>
//{
//    var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
//    {
//        DateTimeFormat = "HH:mm"
//    };
//    options.SerializerSettings.Converters.Add(dateConverter);
//    //options.SerializerSettings.Culture = new CultureInfo("en-IE");
//    //options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
//}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        optionsBuilder => optionsBuilder.MigrationsAssembly("Infrastructure.EntityFramework"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddCors(option => option.AddDefaultPolicy(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
//builder.Services.AddAutoMapper(typeof(IUserService));???
//builder.Services.AddAutoMapper(typeof(IUserRepository));???

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

//try
//{
//    using (var serviceScope = app.Services.CreateScope())
//    {
//        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
//        await dbContext.Database.EnsureCreatedAsync();
//        //await dbContext.Database.MigrateAsync();
//    }
//}
//catch (Exception e)
//{
//    app.Logger.LogCritical(e, "An exception occurred during the service startup");
//}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.MigrateDatabase<DataContext>();

app.Run();

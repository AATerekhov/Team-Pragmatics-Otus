using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EntityFramework;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using System;
using WebApi.Mapping;
using Services.Implementations.Mapping;
using WebApi.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        optionsBuilder => optionsBuilder.MigrationsAssembly("Infrastructure.EntityFramework"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(IUserService));
builder.Services.AddAutoMapper(typeof(UserMappingProfile));
builder.Services.AddAutoMapper(typeof(UserMappingProfileDto));
builder.Services.AddScoped<IUserService, UserService>();
var app = builder.Build();

try
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        await dbContext.Database.EnsureCreatedAsync();
        //await dbContext.Database.MigrateAsync();
    }
}
catch (Exception e)
{
    app.Logger.LogCritical(e, "An exception occurred during the service startup");
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MigrateDatabase<DataContext>();

app.Run();

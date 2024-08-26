using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserApi;
using UserApi.DataAccess.BusinessLogic.Services;
using UserApi.DataAccess.BusinessLogic.Services.Base;
using UserApi.DataAccess.Entities;
using UserApi.DataAccess.EntityFramework;
using UserApi.DataAccess.Repositories.Abstractions;
using UserApi.DataAccess.Repositories.Implementations;
using UserApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices(builder.Configuration);
builder.Services.AddMassTransit(x => {
    x.UsingRabbitMq((context, cfg) =>
    {
        Registrar.ConfigureRmq(cfg, builder.Configuration);
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(IUserService));
builder.Services.AddScoped<IUserService, UserService>();


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
app.MigrateDatabase<DataContext>();
app.Run();

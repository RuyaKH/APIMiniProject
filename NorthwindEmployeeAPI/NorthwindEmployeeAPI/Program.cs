using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NorthwindAPI.Services;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Data.Repository;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var dbConnection = builder.Configuration["DefaultConnection"];

builder.Services.AddDbContext<NorthwindContext>(
opt => opt.UseSqlServer(dbConnection));

builder.Services.AddControllers()
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

//builder.Services.AddAutoMapper(typeof(Program));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped(typeof(INorthwindService<Employee>), typeof(EmployeeService));
builder.Services.AddScoped(typeof(INorthwindService<Order>), typeof(OrderService));
builder.Services.AddScoped<INorthwindRepository<Order>, OrderRepository>();


builder.Services.AddScoped(typeof(INorthwindRepository<>), typeof(NorthwindRepository<>));
builder.Services.AddScoped(typeof(INorthwindService<>), typeof(NorthwindServices<>));

builder.Services.AddScoped<INorthwindRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<INorthwindRepository<Territory>, TerritoryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

//app.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapControllers();

app.Run();

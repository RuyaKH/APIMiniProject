using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Services;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Data.Repository;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConnection = builder.Configuration["DefaultConnection"];
builder.Services.AddDbContext<NorthwindContext>(
opt => opt.UseSqlServer(dbConnection));

builder.Services.AddControllers()
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(INorthwindRepository<>), typeof(NorthwindRepository<>));
builder.Services.AddScoped(typeof(INorthwindService<>), typeof(NorthwindService<>));

builder.Services.AddScoped<INorthwindRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<INorthwindService<Employee>, EmployeeServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

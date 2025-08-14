using Store.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Setting connection string for postgre sql
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adding controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI for testing apis
app.UseSwagger();
app.UseSwaggerUI();

// Mapping controllers
app.MapControllers();

app.Run();
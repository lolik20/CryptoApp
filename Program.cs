﻿using CryptoCalculator;
using CryptoCalculator.Interfaces;
using CryptoCalculator.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.DescribeAllParametersInCamelCase());
builder.Services.AddTransient<IXeService, XeService>();
builder.Services.AddTransient<IBalanceService, BalanceService>();
builder.Services.AddTransient<IExchangeService, ExchangeService>();
builder.Services.AddDbContext<ApplicationContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddHttpClient<IXeService, XeService>(client =>
{
    IConfigurationSection xeConfig = builder.Configuration.GetSection("Xe");
    string host = xeConfig.GetValue<string>("host") ?? throw new Exception("Host missing");
    client.BaseAddress = new Uri($"https://{host}");
    string login = xeConfig.GetValue<string>("Login") ?? throw new Exception("Login missing");
    string password = xeConfig.GetValue<string>("Password") ?? throw new Exception("Password missing");
    string token = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{login}:{password}"));
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", token);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
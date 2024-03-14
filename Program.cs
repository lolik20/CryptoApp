using CryptoExchange;
using CryptoExchange.Commands;
using CryptoExchange.Interfaces;
using CryptoExchange.Queries;
using CryptoExchange.RequestModels;
using CryptoExchange.Services;
using CryptoExchange.Workers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TradePack.Options;
using TradePack.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.DescribeAllParametersInCamelCase());
builder.Services.AddMemoryCache();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IHostedService, PaymentWorker>();
builder.Services.AddTransient<IEthService, EthService>();
builder.Services.AddByBitService();
builder.Services.AddXeService(xeServiceOptions: builder.Configuration.GetSection("Xe").Get<XeServiceOptions>());
builder.Services.AddDbContext<ApplicationContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("Database")), ServiceLifetime.Transient, ServiceLifetime.Transient);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetUserPaymentQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreatePaymentCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdatePaymentCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(BalanceQuery).Assembly);


});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseAuthorization();

app.MapControllers();

app.Run();

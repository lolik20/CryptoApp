using CryptoExchange;
using CryptoExchange.Commands;
using CryptoExchange.Interfaces;
using CryptoExchange.Queries;
using CryptoExchange.RequestModels;
using CryptoExchange.Services;
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
builder.Services.AddTransient<ICurrencyService, CurrencyService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddDbContext<ApplicationContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddXeService(builder.Configuration.GetValue<XeServiceOptions>("Xe"));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ConvertCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(TopUpCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(WithdrawCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(PaymentQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreatePaymentCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdatePaymentCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(BalanceQuery).Assembly);


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseAuthorization();

app.MapControllers();

app.Run();

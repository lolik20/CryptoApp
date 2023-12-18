using CryptoExchange.Exceptions;
using System.Net;

namespace CryptoExchange
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BaseException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, BaseException exception)
        {
            context.Response.StatusCode = exception.GetStatusCode();
            context.Response.ContentType = "text/plain";
            _logger.LogError("Exception: {0}, {1}", exception.Message, DateTime.UtcNow);
            int statusCode = exception.GetStatusCode();
            if (statusCode >=500 &&statusCode<=599)
            {
                await context.Response.WriteAsync("Internal exception");
            }
            await context.Response.WriteAsync(exception.Message);

        }
    }
}

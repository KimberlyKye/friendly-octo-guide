using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
   public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"Request: {context.Request.Path}");

        try
        {
            await _next(context);
            _logger.LogInformation($"Response: {context.Response.StatusCode}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            throw; // или обработать ошибку
        }
    }
}
}
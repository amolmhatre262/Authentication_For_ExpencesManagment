using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace ExpenceManagment_AuthenticationSerivices.Middlewares
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Call next middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log full exception for developers (via Serilog)
                _logger.LogError(ex,
                    "Unhandled exception occurred. Path: {Path}, TraceIdentifier: {TraceId}",
                    context.Request.Path,
                    context.TraceIdentifier);

                // Build user-friendly error response
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Something went wrong. Please try again later.",
                    TraceId = context.TraceIdentifier  // Helpful for developers to trace
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }

        }
    }
}

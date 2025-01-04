using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MiAplicacion.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MiAplicacion.Middleware
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
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Resource not found");
                await HandleExceptionAsync(httpContext, ex.StatusCode, ex.Message);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error");
                await HandleExceptionAsync(httpContext, ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error");
                await HandleExceptionAsync(httpContext, (int)HttpStatusCode.InternalServerError,
                    "An unexpected error occurred. Please try again later.");
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            var errorResponse = new
            {
                StatusCode = statusCode,
                Message = message,  
                TraceId = context.TraceIdentifier 
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}


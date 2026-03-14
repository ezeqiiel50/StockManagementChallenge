using System.Net;
using System.Text.Json;

namespace StockManager.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,"Excepcion no manejada en {Method} {Path}",context.Request.Method, context.Request.Path);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                value = (object?)null,
                errors = new[]
                {
                    new
                    {
                        message   = "Ocurrió un error interno."
                    }
                },
                success = false
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
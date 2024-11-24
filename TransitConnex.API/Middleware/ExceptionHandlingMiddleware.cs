using System.Net;
using System.Text.Json;

namespace TransitConnex.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex) // Gonna be using these for 404 NotFound
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound);
        }
        // catch (Exception ex)
        // {
        //     context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        //     await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError);
        // }
    }

    private static async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";

        var response = new {error = new {message, statusCode = (int)statusCode, timestamp = DateTime.UtcNow}};

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

using System.Net;
using System.Text.Json;

namespace ITRental.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; 
        var result = exception.Message;

        if (exception is KeyNotFoundException)
        {
            code = HttpStatusCode.NotFound;
        }
        else if (exception is ApplicationException)
        {
            code = HttpStatusCode.BadRequest;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        var responsePayload = JsonSerializer.Serialize(new { error = result });
        return context.Response.WriteAsync(responsePayload);
    }
}





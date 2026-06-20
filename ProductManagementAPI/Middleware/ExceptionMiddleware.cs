using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProductManagementAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode =
                (int)HttpStatusCode.NotFound;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    new
                    {
                        message = ex.Message
                    }));
        }
        catch (Exception ex)
        {
            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    new
                    {
                        message = ex.Message
                    }));
        }
    }
}
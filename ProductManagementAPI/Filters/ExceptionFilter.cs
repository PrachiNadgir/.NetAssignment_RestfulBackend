using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductManagementAPI.Filters;

public class ExceptionFilter
    : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter>
        _logger;

    public ExceptionFilter(
        ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(
        ExceptionContext context)
    {
        _logger.LogError(
            context.Exception,
            "Unhandled Exception Occurred");

        if (context.Exception
            is NotFoundException)
        {
            context.Result =
                new NotFoundObjectResult(
                    new
                    {
                        Message =
                            context.Exception.Message
                    });

            context.ExceptionHandled = true;

            return;
        }

        if (context.Exception
            is ValidationException)
        {
            context.Result =
                new BadRequestObjectResult(
                    new
                    {
                        Message =
                            context.Exception.Message
                    });

            context.ExceptionHandled = true;

            return;
        }

        context.Result =
            new ObjectResult(
                new
                {
                    Message =
                        "Internal Server Error"
                })
            {
                StatusCode = 500
            };

        context.ExceptionHandled = true;
    }
}
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ProductManagementAPI.Filters;

public class LoggingFilter : IActionFilter
{
    private readonly ILogger<LoggingFilter> _logger;

    public LoggingFilter(
        ILogger<LoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(
        ActionExecutingContext context)
    {
        _logger.LogInformation(
            "Executing Action: {ActionName}",
            context.ActionDescriptor.DisplayName);
    }

    public void OnActionExecuted(
        ActionExecutedContext context)
    {
        _logger.LogInformation(
            "Executed Action: {ActionName}",
            context.ActionDescriptor.DisplayName);
    }
}
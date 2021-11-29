using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace _01.Filters
{
    public class LoggingFilter : IActionFilter
    {
        private readonly ILogger<LoggingFilter> _logger;

        public LoggingFilter(ILogger<LoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation(
                "Calling action {Action} with arguments {Arguments}",
                context.ActionDescriptor.DisplayName,
                context.ActionArguments);
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation(
                "Action {Action} completed with result {Result}",
                context.ActionDescriptor.DisplayName,
                context.Result);
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace TestApplication.Filters
{
    public class LoggingFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resContext = await next();
        }
    }
}
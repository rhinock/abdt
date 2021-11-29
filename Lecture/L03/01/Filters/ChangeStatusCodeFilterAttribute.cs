using _01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace _01.Filters
{
    public class ChangeStatusCodeFilterAttribute : Attribute, IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult { Value: ApiResult { ErrorCode: > 0 } })
                context.HttpContext.Response.StatusCode = 400;
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}

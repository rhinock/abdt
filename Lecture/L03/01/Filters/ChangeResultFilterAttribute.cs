using _01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace _01.Filters
{
    public class ChangeResultFilterAttribute : Attribute, IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult okObjectResult)
                if (okObjectResult.Value is ExampleResponse exampleResponse)
                    exampleResponse.Id = 999999;
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}

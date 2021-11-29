using _01.Filters;
using _01.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace _01.Controllers
{
    [ApiController]
    [Route("api")]
    [ChangeResultFilter]
    public class ApiController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Index()
        {
            return Ok(new { Test = "data" });
        }

        /// <summary>
        /// Пример атрибутов валидации
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public IActionResult AddUser(AddUserViewModel request)
        {
            return Ok(new { Test = "data" });
        }

        /// <summary>
        /// Пример атрибутов валидации
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpPost("customuser")]
        public IActionResult AddUserWithId(
            [FromQuery] string id,
            [FromBody] AddUserViewModel request,
            [FromHeader(Name = "Accept-Language")] string lang)
        {
            return Ok(new { Test = "data" });
        }

        [HttpGet("user/{id:long}/{name:length(4,25)}")]
        public IActionResult GetUser([FromRoute] long id, string name)
        {
            return Ok(new { Test = "data" });
        }

        [HttpGet("user/{id:guid}")]
        public IActionResult GetUserWithGuid(Guid id)
        {
            return Ok(new { Test = "data" });
        }

        [HttpGet("[action]")]
        public ExampleResponse Example1()
        {
            if (false)
            {
                // ошибка
                // return BadRequest();
            }

            return new ExampleResponse() { Id = 1 };
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(ExampleResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Example2()
        {
            if (false)
                return BadRequest(new { Test = 1 });

            return Ok(new ExampleResponse() { Id = 1 });
        }

        [HttpGet("[action]")]
        public ActionResult<ExampleResponse> Example3()
        {
            if (false)
                return BadRequest();

            return new ExampleResponse() { Id = 1 };
        }

        [HttpGet("[action]")]
        public ActionResult<ApiResult<ExampleResponse>> ExampleApiResponse()
        {
            if (false)
            {
                // ошибка
            }

            return new ApiResult<ExampleResponse>() { ErrorCode = 10, ErrorMessage = "Some error" };

            // return new ApiResult<ExampleResponse> { Result = new ExampleResponse() { Id = 2 };
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _07.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("example")]
        public IActionResult Index()
        {
            return Ok(new { Test = 1 });
        }
    }
}

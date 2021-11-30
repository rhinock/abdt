using Microsoft.AspNetCore.Mvc;

namespace _12.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok(new { Test = 1 });
        }
    }
}

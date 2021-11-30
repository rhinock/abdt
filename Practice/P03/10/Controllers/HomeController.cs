using _10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace _10.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<TestConfig> _config;

        public HomeController(IOptions<TestConfig> config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_config);
        }
    }
}

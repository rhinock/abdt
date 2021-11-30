using _11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace _11.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<CardsOptions> _config;

        public HomeController(IOptions<CardsOptions> config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return new JsonResult(_config);
        }
    }
}

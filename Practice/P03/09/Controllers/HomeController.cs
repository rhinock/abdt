using _09.Models;
using Microsoft.AspNetCore.Mvc;

namespace _09.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(EditViewModel model)
        {
            return View();
        }
    }
}

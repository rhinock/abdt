using Microsoft.AspNetCore.Mvc;
using System;

namespace _01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            throw new Exception("Test");
        }
    }
}

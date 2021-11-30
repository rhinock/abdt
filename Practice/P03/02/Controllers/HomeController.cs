using _02.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace _02.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            throw new Exception("Test");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

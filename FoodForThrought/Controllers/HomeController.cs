using FoodForThrought.Data;
using FoodForThrought.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodForThrought.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly RegisterDbcontext _registerDbcontext;

        public HomeController(ILogger<HomeController> logger, RegisterDbcontext registerDbcontext)
        {
            _logger = logger;
            _registerDbcontext = registerDbcontext;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Catalogue()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Single_catalouge()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult login_user(Login login)
        {
            return RedirectToAction("Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
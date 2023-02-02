using FoodForThrought.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodForThrought.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult Upload_product(AddProductModel product)
        {
            return View();
        }
    }
}

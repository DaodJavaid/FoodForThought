using Microsoft.AspNetCore.Mvc;

namespace FoodForThrought.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult AddProduct()
        {
            return View();
        } 
        
        public IActionResult UpdateProduct()
        {
            return View();
        }
        public IActionResult DeleteProduct()
        {
            return View();
        }
        public IActionResult SearchProduct()
        {
            return View();
        }
        public IActionResult ViewAllProduct()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace FoodForThrought.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserDeshboard()
        {
            return View();
        }
    }
}

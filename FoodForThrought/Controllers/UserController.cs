using FoodForThrought.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodForThrought.Controllers
{
    public class UserController : Controller
    {
        public readonly RegisterDbcontext _user;
        public UserController(RegisterDbcontext user)
        {
            _user = user;
        }
        public IActionResult UserDeshboard()
        {
            return View();
        }
        public async Task<IActionResult>  ShowUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _user.Signup.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public IActionResult UpdateUser()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FoodForThrought.Models.Admin;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FoodForThrought.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public IActionResult AdminDeshboard()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if(!claimUser.Identity.IsAuthenticated)
            {
                // User is authorized to access this action
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        public IActionResult AdminLogin()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogincheck(AdminLogin logindata)
        {
            if(logindata.email == "123@123" && logindata.Password == "123")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, logindata.email),
                    new Claim("OtherProperties", "admin"),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);


                AuthenticationProperties properties = new AuthenticationProperties() {
                
                    AllowRefresh= true,
                    IsPersistent= true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                // User is authorized to access this action
                return RedirectToAction("AdminDeshboard", "Admin");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Home", "Home");
        }
    }
}

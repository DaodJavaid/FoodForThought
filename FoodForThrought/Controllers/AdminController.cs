using FoodForThrought.Data;
using FoodForThrought.Migrations;
using FoodForThrought.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Net;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using FoodForThrought.Models.Admin;
using System.Security.Claims;

namespace FoodForThrought.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public IActionResult AdminDeshboard()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.HideMenuBar = true;

                // Get the user's email address
                string email = User.Identity.Name;

                // Get the user's roles
                var roles = ((ClaimsIdentity)User.Identity).Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value);
                // User is authorized to access this action
                return RedirectToAction("AdminDeshboard", "Admin");
            }
            else
            {
                // User is not authorized to access this action, redirect to error page or login page
                return RedirectToAction("AdminLogin", "Admin");
            }

            return View();
        }

        public IActionResult AdminLogin()
        {
            return View();
        }


        public IActionResult AdminLogincheck(AdminLogin logindata)
        {
            if(logindata.email == "123@123" && logindata.Password == "123")
            {
                return RedirectToAction("AdminDeshboard", "Admin");
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }


            return View();
        }
    }
}

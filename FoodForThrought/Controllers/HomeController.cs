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



        [HttpPost]
        public IActionResult Register_user(Register register)
        {

            var check_registration = _registerDbcontext.Signup.ToList();

            if (check_registration != null)
            {
                foreach (var getdata in check_registration)
                {
                    String mail = getdata.email;


                    if (register.email == mail )
                    {
                        TempData["confirm"] = "Email Already Exit";
                        return RedirectToAction("Register");
                    }
                }
            }



            if (ModelState.IsValid)
            {
                if (register.password == register.confirm_password)
                {
                    try
                    {
                        _registerDbcontext.Add(register);
                        _registerDbcontext.SaveChanges();
                        TempData["confirm"] = "Signup Successfully";
                    }
                    catch (Exception ex)
                    {
                        TempData["confirm"] = "There is Some Error. user Not register";

                    }
                }
                else
                {
                    TempData["confirm"] = "Password And Confirm Password Is Not Same";
                    return RedirectToAction("Register");
                }


            }
            return RedirectToAction("Login");
        }



        [HttpPost]
        public IActionResult login_user(Login login)
        {
            var check_registration = _registerDbcontext.Signup.ToList();

            if (check_registration != null)
            {
                foreach(var getdata in check_registration)
                {
                    String mail = getdata.email;
                    String pass = getdata.password;


                    if(login.email == mail && login.Password == pass)
                    {
                        TempData["confirm"] = "Login Successfully";
                        break;
                    }
                    else
                    {
                            TempData["confirm"] = "Email and Password Incorrect";
                    }
                }
            }


            return RedirectToAction("Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
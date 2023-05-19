using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using FoodForThrought.Models;
using FoodForThrought.Data;
using Microsoft.AspNetCore.Hosting;

namespace FoodForThrought.Controllers
{
    
    public class AdminController : Controller
    {

        public readonly AdminDbContext _adminDbContext;

        // Check how manay User Register
        public readonly RegisterDbcontext _registerDbcontext;

        // Check how manay Product Add in database
        public readonly ProductimageDbcontext _displayProductnow;

        public AdminController(AdminDbContext adminDbContext,
               RegisterDbcontext registerDbcontext,
               ProductimageDbcontext displayProductnow)
        {
            _adminDbContext = adminDbContext;
            _registerDbcontext = registerDbcontext;
            _displayProductnow = displayProductnow;
        }
       
        [Authorize(AuthenticationSchemes = "AdminAuthentication")]
        public IActionResult AdminDeshboard()
        {
            
            // Check how manay User Register
            int Usercount = _registerDbcontext.Signup.Count();
            var ShowUser = _registerDbcontext.Signup.ToList();
            ViewBag.Usercount = Usercount;

            // Check how manay Product Add in database
            int Productcount = _displayProductnow.AddingProduct.Count();
            ViewBag.Productcount = Productcount;

            return View(ShowUser);
        }

        public IActionResult AdminLogin()
        {
           return View();
        }

         public IActionResult AdminRegister()
        {
           return View();
        }

         [HttpPost]
         public async Task<IActionResult> AdminLogincheck(AdminLogin1 Adminlogin1)
         {

            var check_registration = _adminDbContext.AdminCheck.ToList();

            if (check_registration != null)
            {
                foreach (var getdata in check_registration)
                {
                    String mail = getdata.admin_email;
                    String pass = getdata.admin_password;


                    if (Adminlogin1.admin_email == mail && Adminlogin1.admin_Password == pass)
                    {
                        List<Claim> claims = new List<Claim>()
                             {
                              new Claim(ClaimTypes.NameIdentifier, Adminlogin1.admin_email),
                              new Claim("OtherProperties", "admin"),
                             };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                      claims, "AdminAuthentication");


                        AuthenticationProperties properties = new AuthenticationProperties()
                        {

                            AllowRefresh = true,
                            IsPersistent = true,
                        };

                        await HttpContext.SignInAsync("AdminAuthentication",
                             new ClaimsPrincipal(claimsIdentity), properties);
                        TempData["confirm"] = "Login Successfully";
                        break;
                    }
                    else
                    {
                        TempData["confirm"] = "Email and Password Incorrect";
                        return RedirectToAction("AdminLogin", "Admin");
                    }
                }
            }

            return RedirectToAction("AdminDeshboard", "Admin");
        }
     
        [HttpPost]
        public IActionResult Register_Admin(AdminRegister1 Admin_register)
        {

            var check_registration = _adminDbContext.AdminCheck.ToList();

            if (check_registration != null)
            {
                foreach (var getdata in check_registration)
                {
                    String mail = getdata.admin_email;


                    if (Admin_register.admin_email == mail)
                    {
                        TempData["confirm"] = "Email Already Exit";
                        return RedirectToAction("Register");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                if (Admin_register.admin_password == Admin_register.admin_confirm_password)
                {
                    try
                    {
                        _adminDbContext.Add(Admin_register);
                        _adminDbContext.SaveChanges();
                        TempData["confirm"] = "Signup Successfully";
                    }
                    catch (Exception)
                    {
                        TempData["confirm"] = "There is Some Error. user Not register";

                    }
                }
                else
                {
                    TempData["confirm"] = "Password And Confirm Password Is Not Same";
                    return RedirectToAction("AdminRegister");
                }


            }
            return RedirectToAction("AdminLogin");
        }


        [Authorize(AuthenticationSchemes = "AdminAuthentication")]
        public async Task<IActionResult> AdminLogOut()
        {
            await HttpContext.SignOutAsync("AdminAuthentication");
            return RedirectToAction("Home", "Home");
        }
    }
}

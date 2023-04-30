using FoodForThrought.Data;
using FoodForThrought.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;

namespace FoodForThrought.Controllers
{
    public class RegisterUser : Controller
    {

        // Check how manay User Register
        public readonly RegisterDbcontext _registerDbcontext;


        public RegisterUser(RegisterDbcontext registerDbcontext)
        {
            _registerDbcontext = registerDbcontext;
        }


        public IActionResult AddRegisterUser()
        {
            return View();
        }
        public IActionResult UpdateRegisterUser()
        {
            return View();
        }
        public IActionResult DeleteRegisterUser()
        {
            return View();
        }
        public IActionResult ViewAllRegisterUser()
        {
            return View();
        }
        public IActionResult SearchRegisterUser()
        {
            return View();
        }

       /* [HttpPost]
        public IActionResult Register_user_from_Admin(Adduserfromadmin Adduserfromadmin)
        {

            var check_registration = _registerDbcontext.Signup.ToList();

            if (check_registration != null)
            {
                foreach (var getdata in check_registration)
                {
                    String mail = getdata.email;


                    if (Adduserfromadmin.email == mail)
                    {
                        TempData["confirm"] = "Email Already Exit";
                        return RedirectToAction("AddRegisterUser");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                if (Adduserfromadmin.password == Adduserfromadmin.confirm_password)
                {
                    try
                    {
                        _registerDbcontext.Add(Adduserfromadmin);
                        _registerDbcontext.SaveChanges();
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
                    return RedirectToAction("AddRegisterUser");
                }


            }
            return RedirectToAction("AddRegisterUser");
        }
*/


    }
}

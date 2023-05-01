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
        public IActionResult SearchandUpdateRegisterUser()
        {
            var SearchandUpdate_user = _registerDbcontext.Signup.ToList();

            return View(SearchandUpdate_user);
        }
        public IActionResult DeleteRegisterUser()
        {
            var SearchandUpdate_user = _registerDbcontext.Signup.ToList();

            return View(SearchandUpdate_user);
        }
        public IActionResult ViewAllRegisterUser()
        {
            var SearchandUpdate_user = _registerDbcontext.Signup.ToList();

            return View(SearchandUpdate_user);
        }
        public IActionResult Deleting_user(AdminRegister user)
        {
           

                var user_delete = _registerDbcontext.Signup.ToList();

                if (user_delete != null)
                {
                    foreach(var getdata in user_delete)
                    {
                        String mail = getdata.email;


                        if (user.email_old == mail)
                        {
                            _registerDbcontext.Signup.Remove(user);
                            _registerDbcontext.SaveChanges();
                            TempData["confirm"] = "User Deleted Successfully";
                        }
                        else
                        {
                            TempData["confirm"] = "User Not Found";
                        }
                    }
                }
                else
                {
                    TempData["confirm"] = "There is Some Error. user_delete Not Valid";
                }
            
            return RedirectToAction("DeleteRegisterUser");
        }

    }
}

using FoodForThrought.Data;
using FoodForThrought.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.ComponentModel.DataAnnotations;
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
        public IActionResult SearchandUpdateRegisterUser(AdminRegister searchuser)
        {
            var SearchandUpdate_user = _registerDbcontext.Signup.ToList();

           // var userToSearch = _registerDbcontext.Signup.FirstOrDefault(u => u.email == searchuser.email_old);


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

        [HttpPost]
        public IActionResult Adding_user(AdminRegister addinguserfromadmin)
        {
            var check_registration = _registerDbcontext.Signup.ToList();

            if (check_registration != null)
            {
                foreach (var getdata in check_registration)
                {
                    String mail = getdata.email;

                    if (addinguserfromadmin.email == mail)
                    {
                        TempData["confirm"] = "Email Already Exit";
                        return RedirectToAction("AddRegisterUser");
                    }
                }
            }

            try
            {
                _registerDbcontext.Add(addinguserfromadmin);
                _registerDbcontext.SaveChanges();
                TempData["confirm"] = "User Add Successfully";
            }
            catch (Exception)
            {
                TempData["confirm"] = "There is Some Error. user Not register";

            }

            return RedirectToAction("AddRegisterUser");
        }

        public IActionResult Deleting_user(AdminRegister userfromadmin)
        {
            var userToDelete = _registerDbcontext.Signup.FirstOrDefault(u => u.email == userfromadmin.email_old);

            if (userToDelete != null)
            {
                _registerDbcontext.Entry(userToDelete).State = EntityState.Detached; // detach previously tracked entity
                userfromadmin.Id = userToDelete.Id; // set the Id of the entity to be deleted
                _registerDbcontext.Remove(userfromadmin);
                _registerDbcontext.SaveChanges();
                TempData["confirm"] = "User Deleted Successfully";
            }
            else
            {
                TempData["confirm"] = "User Not Found";
            }

            return RedirectToAction("DeleteRegisterUser");
        }

        public async Task<IActionResult> Update_user(AdminRegister udpateuser)
        {
            var existingUser = _registerDbcontext.Signup.FirstOrDefault(u => u.email == udpateuser.email_old);

            if (existingUser != null)
            {
                if (udpateuser.password == udpateuser.confirm_password)
                {
                    existingUser.username = udpateuser.username;
                    existingUser.email = udpateuser.email_old;
                    existingUser.password = udpateuser.password;
                    existingUser.confirm_password = udpateuser.confirm_password;
                    existingUser.address = udpateuser.address;
                    existingUser.country = udpateuser.country;
                    existingUser.city = udpateuser.city;
                    existingUser.zip = udpateuser.zip;
                    existingUser.gender = udpateuser.gender;

                    try
                    {
                        _registerDbcontext.Update(existingUser);
                        await _registerDbcontext.SaveChangesAsync();
                        TempData["confirm"] = "User Updated Successfully";
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                        {
                            ViewBag.ErrorMessage = e.InnerException.Message;
                        }
                        else
                        {
                            ViewBag.ErrorMessage = e.Message;
                        }
                        TempData["confirm"] = "There is some error. User not updated.";
                    }
                }
                else
                {
                    TempData["confirm"] = "Password and Confirm Password Did Not Same";
                }
            }
            else
            {
                TempData["confirm"] = "User Not Found";
            }

            return RedirectToAction("SearchandUpdateRegisterUser");
        }

        public IActionResult Search(AdminRegister searchuser)
        {

            // var user = _registerDbcontext.Signup.Find(searchuser.email_old);
            var user = _registerDbcontext.Signup.FirstOrDefault(u => u.email == searchuser.email_old);


            if (user == null)
            {
                // User not found

                TempData["confirm"] = "User not found";
                return View();
            }

            TempData["UserName"] = user.username;
            TempData["UserEmail"] = user.email;
            TempData["UserAddress"] = user.address;
            TempData["UserCountry"] = user.country;
            TempData["UserCity"] = user.city;
            TempData["UserZip"] = user.zip;



            // Pass the user data to the view
            return RedirectToAction("SearchandUpdateRegisterUser");
        }
    }
}

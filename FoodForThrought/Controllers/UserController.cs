using FoodForThrought.Data;
using FoodForThrought.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodForThrought.Controllers
{
    [Authorize]
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
        [Authorize]
        public IActionResult ShowUser()
        {
            // Fetch the email of the logged-in user from claims
            string userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userEmail))
            {
                // No user is logged in.
                return RedirectToAction("ShowUser");
            }

            // Fetch user details from the database using the email
            var userDetails = _user.Signup.FirstOrDefault(u => u.email == userEmail);

            if (userDetails == null)
            {
                // User details not found in the database. Handle this case as needed.
                return View("Error");
            }
            TempData["UserName"] = userDetails.username;
            TempData["UserEmail"] = userDetails.email;
            TempData["UserAddress"] = userDetails.address;
            TempData["UserCountry"] = userDetails.country;
            TempData["UserCity"] = userDetails.city;
            TempData["UserZip"] = userDetails.zip;
            TempData["UserGender"] = userDetails.gender;

            // Pass the user data to the view
            return View(userDetails);
        }

        public IActionResult UpdateUser()
        {
            return View();
        }

        public IActionResult Search(AdminRegister searchuser)
        {

            // var user = _registerDbcontext.Signup.Find(searchuser.email_old);
            var user = _user.Signup.FirstOrDefault(u => u.email == searchuser.email_old);


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
            return RedirectToAction("UpdateUser");
        }

        public async Task<IActionResult> Update_user(AdminRegister udpateuser)
        {
            var existingUser = _user.Signup.FirstOrDefault(u => u.email == udpateuser.email_old);

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
                        _user.Update(existingUser);
                        await _user.SaveChangesAsync();
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

            return RedirectToAction("ShowUser");
        }

    }
}

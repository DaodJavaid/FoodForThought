using FoodForThrought.Data;
using FoodForThrought.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;

namespace FoodForThrought.Controllers
{
    public class MessageController : Controller
    {

        public readonly ContactDbcontext _contactDbcontext;

        public MessageController(ContactDbcontext contactDbcontext)
        {
            _contactDbcontext = contactDbcontext;
        }
        public IActionResult ViewAllMessage()
        {

            var All_message = _contactDbcontext.Message.ToList();

            return View(All_message);
        }

        public IActionResult DeleteMessage()
        {
            var All_message = _contactDbcontext.Message.ToList();

            return View(All_message);
        }

        [HttpPost]
        public IActionResult DeletingMessage(ContactUsForm contactus)
        {

            try
            {
                var contactUs = _contactDbcontext.Message.Find(contactus.user_email);

                if (contactUs != null)
                {
                    _contactDbcontext.Remove(contactUs);
                    _contactDbcontext.SaveChanges();
                    TempData["confirm"] = "Mesage Deleted Successfully";
                }
                else
                {
                    TempData["confirm"] = "Email Not Found";
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g. log it)
                TempData["confirm"] = "Error Deleting Contact Us Data";
            }

            return RedirectToAction("DeleteMessage");
        }


    }
}

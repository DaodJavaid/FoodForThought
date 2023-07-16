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
        public IActionResult Deleting_Message(ContactUsForm Message)
        {
            try
            {
                var messages = _contactDbcontext.Message.Find(Message.old_user_email);
                if (messages != null)
                {
                    _contactDbcontext.Message.Remove(messages);
                    _contactDbcontext.SaveChanges();
                    TempData["confirm"] = "Message Deleted Successfully";
                }
                else
                {
                    TempData["confirm"] = "Message Not Found";
                }
            }
            catch (Exception)
            {
                TempData["confirm"] = "There is Some Error. Message Not Deleted";
            }

            return RedirectToAction("DeleteMessage");
        }

    }
}

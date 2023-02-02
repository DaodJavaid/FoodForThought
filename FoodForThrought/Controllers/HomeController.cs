using FoodForThrought.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;

namespace FoodForThrought.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public IActionResult Contact_us_form(ContactUsForm userData)
        {
            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com");
                    client.Authenticate("19101001-033@uskt.edu.pk", "hpfrbrdybyaasale");

                    String info = "Thank you for reaching out to Food For Tought! \r\n " +
                                   "Be assured, you are in good hands and we are working hard to support you in the best way.\r\n" +
                                   "It usually takes us about 12 hours to respond but we will do our best to get back to you sooner.\r\n" +
                                   "We will be in touch with you ASAP,\r\nThe Food For Tought! ";

                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = $"<p>{info}</p> <p>{userData.user_message}</p>",
                        TextBody = "{info} \r\n {userData.user_message}"
                    };

                    String client_mail = userData.user_email;

                    var messege = new MimeMessage
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };
                    messege.From.Add(new MailboxAddress("Food For Tought", "19101001-033@uskt.edu.pk"));
                    messege.To.Add(new MailboxAddress(userData.user_name, client_mail));
                    messege.Subject = "Food For Tought Support Team (Food For Tought Customer Support) ";
                    client.Send(messege);

                    client.Disconnect(true);
                }

                TempData["Check"] = "1";
            }
            catch (SmtpFailedRecipientException ex)
            {
                // ex.FailedRecipient and ex.GetBaseException() should give you enough info.

                TempData["Check"] = "0";

            }


            return RedirectToAction("Contact");
        }

        [HttpPost]
        public IActionResult Register_user(Register signup)
        {
            return RedirectToAction("Home");
        }

        [HttpPost]
        public IActionResult login_user(Login login)
        {
            return RedirectToAction("Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
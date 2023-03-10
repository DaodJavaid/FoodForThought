using FoodForThrought.Data;
using FoodForThrought.Migrations;
using FoodForThrought.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Diagnostics;
using System.Net.Mail;

namespace FoodForThrought.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly RegisterDbcontext _registerDbcontext;

        public readonly ContactDbcontext _contactDbcontext;

        public HomeController(ILogger<HomeController> logger,
               RegisterDbcontext registerDbcontext, ContactDbcontext contactDbcontext)
        {
            _logger = logger;
            _registerDbcontext = registerDbcontext;
            _contactDbcontext =  contactDbcontext;
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

        public IActionResult Contact_us_form(ContactUsForm contactus)
        {

            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com");
                    client.Authenticate("19101001-033@uskt.edu.pk", "hpfrbrdybyaasale");

                    String System_message = "Thank you for reaching out to Food For Tought! Be" +
                        " assured, you are in good hands and we are working hard to support you " +
                        "in the best way. It usually takes us about 12 hours to respond but we" +
                        " will do our best to get back to you sooner. We will be in touch with " +
                        "you ASAP, The Food For Tought!";

                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = $"<p>{System_message}</p> <p>{contactus.user_message}</p>",
                        TextBody = System_message + " \r\n {contactus.user_message}"
                    };

                    var messege = new MimeMessage
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };
                    messege.From.Add(new MailboxAddress("Food For Tought", "19101001-033@uskt.edu.pk"));
                    messege.To.Add(new MailboxAddress(contactus.user_name, contactus.user_email));
                    messege.Subject = "Food For Tought Support Team (Food For Tought Customer Support)";
                    client.Send(messege);

                    client.Disconnect(true);

                    TempData["confirm"] = "Done";
                }
            }
            catch (SmtpFailedRecipientException ex)
            {
               
                TempData["confirm"] = "Not";
            }

            if(TempData["confirm"] == "Done")
            {


            if (ModelState.IsValid)
            {
                   try
                    {
                        _contactDbcontext.Add(contactus);
                        _contactDbcontext.SaveChanges();
                        TempData["confirm"] = "Message Send Successfully";
                    }
                    catch (Exception ex)
                    {
                        TempData["confirm"] = "Message Didn't Send Successfully";

                    }            
            }

            }
            else
            {
                TempData["confirm"] = "Message Didn't Send Successfully";
            }

            return RedirectToAction("Contact");
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
                    catch (Exception)
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
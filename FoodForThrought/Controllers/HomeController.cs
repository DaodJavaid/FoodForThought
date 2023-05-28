using FoodForThrought.Data;
using FoodForThrought.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.ML;
using System.Drawing;
using Microsoft.ML.Data;
using Microsoft.IdentityModel.Tokens;

namespace FoodForThrought.Controllers
{

  //  facial expression in to one of four categories(0=Angry, 1=Fear, 3=Happy, 4=Sad).
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly RegisterDbcontext _registerDbcontext;
        public readonly ContactDbcontext _contactDbcontext;
        public readonly ProductimageDbcontext _displayProductnow;
        public readonly QuestionnaireDbContext _questionnaireDbContext;



        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger,
               RegisterDbcontext registerDbcontext, 
               ContactDbcontext contactDbcontext,
               ProductimageDbcontext displayProductnow,
               QuestionnaireDbContext questionnaireDbContext,
               IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _registerDbcontext = registerDbcontext;
            _contactDbcontext =  contactDbcontext;
            _displayProductnow = displayProductnow;
            _questionnaireDbContext = questionnaireDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Home()
        {
            var show_product = _displayProductnow.AddingProduct.ToList();

            return View(show_product);
        }

        public IActionResult Catalogue()
        {
            var show_product = _displayProductnow.AddingProduct.ToList();

            return View(show_product);
        }

        public IActionResult About()
        {
            return View();
        }
        
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Single_catalouge(int id)
        {
            var product = _displayProductnow.AddingProduct.Find(id);

            return View(product);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "ClientAuthentication")]
        public IActionResult Detectemotion()
        {
           return View();
        }

        [Authorize(AuthenticationSchemes = "ClientAuthentication")]
        public IActionResult Questions()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (!claimUser.Identity.IsAuthenticated)
            {
                // User is authorized to access this action
                return RedirectToAction("Login");
            }

            string imageDirectoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

             string[] imageFiles = Directory.GetFiles(imageDirectoryPath);

             string imagePath_fromfolder = imageFiles[0]; // This will get the path of the first image file in the directory

            Trainmodel(imagePath_fromfolder);

            // Deleting the folder after detect emotion
            string folderPath1 = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            DirectoryInfo di = new DirectoryInfo(folderPath1);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            
            var show_question = _questionnaireDbContext.Question.ToList();


            return View();
        }

        [HttpPost]
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
        public IActionResult Register_user(AdminRegister register)
        {
            // If any property is an empty string, set it to null
            register.address = string.IsNullOrEmpty(register.address) ? null : register.address;
            register.gender = string.IsNullOrEmpty(register.gender) ? null : register.gender;
            register.city = string.IsNullOrEmpty(register.city) ? null : register.city;
            register.country = string.IsNullOrEmpty(register.country) ? null : register.country;
            register.zip = string.IsNullOrEmpty(register.zip) ? null : register.zip;



            var check_registration = _registerDbcontext.Signup.ToList();

            if (check_registration != null)
            {
                foreach (var getdata in check_registration)
                {
                    String mail = getdata.email;

                    if (register.email == mail)
                    {
                        TempData["confirm"] = "Email Already Exit";
                        return RedirectToAction("AddRegisterUser");
                    }
                }
            }

            try
            {
                _registerDbcontext.Add(register);
                _registerDbcontext.SaveChanges();
                TempData["confirm"] = "Create Account Successfully";
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException != null ? ex.InnerException.Message : "";
                TempData["confirm"] = $"There is an error: {ex.Message}. Inner exception: {innerException}. User not registered.";
            }

            return RedirectToAction("Login");
        }


        [HttpPost]
        public async Task<IActionResult> login_user(AdminLogin login)
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
                        List<Claim> claims = new List<Claim>()
                             {
                              new Claim(ClaimTypes.NameIdentifier, login.email),
                              new Claim("OtherProperties", "User"),
                             };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                       claims, "ClientAuthentication");

                        AuthenticationProperties properties = new AuthenticationProperties()
                        {

                            AllowRefresh = true,
                            IsPersistent = true,
                        };

                        await HttpContext.SignInAsync("ClientAuthentication",
                        new ClaimsPrincipal(claimsIdentity), properties);

                        TempData["confirm"] = "Login Successfully";
                        break;
                    }
                    else
                    {
                            TempData["confirm"] = "Email and Password Incorrect";
                    }
                }
            }

            return RedirectToAction("Detectemotion");
        }

        [HttpPost]
        public IActionResult SaveImage(string imageData)
        {
            if(!imageData.IsNullOrEmpty())
            {

                
                string base64Data = imageData.Split(',')[1];
                byte[] bytes = Convert.FromBase64String(base64Data);
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                string fileName = Guid.NewGuid().ToString() + ".png";
                string filePath = Path.Combine(folderPath, fileName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                System.IO.File.WriteAllBytes(filePath, bytes);
                // Wait for 1 second
                // Thread.Sleep(10000); // 10000 milliseconds = 10 second

                //    Trainmodel();
            }

            return Json("Questions");
        }
        [Authorize(AuthenticationSchemes = "ClientAuthentication")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("ClientAuthentication");
            return RedirectToAction("Home", "Home");
        }

        //predict the Emotion on the images
        public void Trainmodel(string imagePath)
        {
           
            // Create a new MLContext
            var mlContext = new MLContext();

            // Converting image to model input
            var input = ConvertImageToInput(imagePath);

            // Load data
            var data = new[] { new ImageData { Conv2DInput = input } };
            var dataView = mlContext.Data.LoadFromEnumerable(data);

            // Define pipeline
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string modelPath_fromfolder = Path.Combine(contentRootPath, "wwwroot", "model", "my_model.onnx");

            var pipeline = mlContext.Transforms.ApplyOnnxModel(
                outputColumnNames: new[] { "dense_1" },
                inputColumnNames: new[] { "conv2d_input" },
                modelFile: modelPath_fromfolder
            );

            // Fit the pipeline to the data
            var model = pipeline.Fit(dataView);

            // Create a prediction engine
            var predictionEngine = mlContext.Model.CreatePredictionEngine<ImageData, EmotionPrediction>(model);

            // Use the model to predict the output of the sample data
            EmotionPrediction prediction = predictionEngine.Predict(new ImageData { Conv2DInput = input });

            var predictedLabelIndex = prediction.PredictedLabels
            .Select((value, index) => new { Value = value, Index = index })
            .Aggregate((a, b) => (a.Value > b.Value) ? a : b)
            .Index;

            ViewBag.PredictedLabel = (EmotionLable)predictedLabelIndex;
        }

        //onvert an image into a grayscale and resize it to 48x48
        private float[] ConvertImageToInput(string imagePath)
        {
            using var bitmap = new Bitmap(System.Drawing.Image.FromFile(imagePath), new Size(48, 48));
            var input = new float[48 * 48];
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var color = bitmap.GetPixel(x, y);
                    var grayScale = (color.R * 0.3) + (color.G * 0.59) + (color.B * 0.11);
                    input[y * bitmap.Width + x] = (float)grayScale / 255;
                }
            }
            return input;
        }

        public class ImageData
        {
            [ColumnName("conv2d_input")]
            [VectorType(1, 48, 48, 1)] // Adjust according to your ONNX model input shape
            public float[] Conv2DInput { get; set; }
        }

        public class EmotionPrediction
        {
            [ColumnName("dense_1")] // Adjust according to your ONNX model output tensor name
            [VectorType(7)] // If your model outputs a probability distribution over 7 classes, adjust if not correct
            public float[] PredictedLabels { get; set; }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
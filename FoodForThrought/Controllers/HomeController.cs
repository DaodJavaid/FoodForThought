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
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;

namespace FoodForThrought.Controllers
{

    //  facial expression in to one of four categories(0=Angry, 1=Fear, 2=Happy, 3=Sad).
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
            _contactDbcontext = contactDbcontext;
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

        public IActionResult Result()
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

            

            var show_question = _questionnaireDbContext.Question.ToList();


            return View(show_question);
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
               
                TempData["confirm"] = ex.Message;
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

            ViewBag.PredictedLabelIndex = predictedLabelIndex;
            ViewBag.PredictedLabel = (EmotionLable)predictedLabelIndex;

            DeleteEmotionFolder();
        }

        public void DeleteEmotionFolder()
        {
            string currentFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            string newFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "newFolderName");

            if(Directory.Exists(newFolderPath))
            {
                Directory.Delete(newFolderPath, true);
            }
            else
            {
                Directory.Move(currentFolderPath, newFolderPath);
            }


            // If the directory exists
            if (Directory.Exists(currentFolderPath))
            {
                string[] files = Directory.GetFiles(currentFolderPath);

                // Loop over the files
                foreach (string file in files)
                {
                    // Attempt to open the file to see if it's in use
                    try
                    {
                        using (var stream = new FileStream(file, FileMode.Open))
                        {
                            // If we can open the file without an exception being thrown, 
                            // it means the file is not in use elsewhere
                            Console.WriteLine($"File {file} is not in use.");
                        }

                        // You might need to add some delay here if you still have issues with deleting files
                        // System.Threading.Thread.Sleep(1000);
                    }
                    catch (IOException)
                    {
                        // If an IOException is thrown, it means the file is in use elsewhere
                        Console.WriteLine($"File {file} is in use. Can't delete it now.");
                        return; // If we can't delete one file, we probably shouldn't delete the folder, so return
                    }
                }

                // If we've gotten to this point, it means all the files are not in use and can be deleted

                // Check if the newFolderPath exists, if not rename currentFolder to newFolder
                if (!Directory.Exists(newFolderPath))
                {
                    Directory.Move(currentFolderPath, newFolderPath);
                }
                else
                {
                    // Delete the directory
                    Directory.Delete(currentFolderPath, true);
                }
            }
        }

        /*      public void DeleteEmotionFolder()
              {
                  string currentFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                  string newFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "newFolderName");

                  // If the destination directory doesn't exist yet, move the directory.
                  // If it does exist, this will throw an exception.
                  do
                  {
                      if (!Directory.Exists(newFolderPath))
                      {
                          Directory.Move(currentFolderPath, newFolderPath);
                      }
                      else
                      {
                          Directory.Delete(newFolderPath, true);
                      }
                  } while (!Directory.Exists(newFolderPath));
              } */

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

        /***************************   Food Prediction  *********************************/

        public async Task<IActionResult> PredictFoodusingAPI(QuestionnaireModel foodans)
        {
            using (var client = new HttpClient())
            {
                // Update with your API's URL
                var url = "http://127.0.0.1:5000/predict";

                var input = new QuestionforPrediction
                {
                    Question1 = ConvertAnswerToFloat(foodans.Fullquestion1, foodans.question1),
                    Question2 = ConvertAnswerToFloat(foodans.Fullquestion2, foodans.question2),
                    Question3 = ConvertAnswerToFloat(foodans.Fullquestion3, foodans.question3),
                    Question4 = ConvertAnswerToFloat(foodans.Fullquestion4, foodans.question4)
                };


                // Serialize input
                var json = JsonConvert.SerializeObject(input);

                // Create StringContent with JSON data
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await client.PostAsync(url, data);

                // Ensure the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and deserialize the response content
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<MyModelOutput>(content);

                    // Use the result
                    ViewBag.Prediction = ConvertPredictionToLabel(result.prediction[0]);

                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ViewBag.Error = "Error: " + response.StatusCode + " " + content;
                }
            }
            return View("Result");
        }

        private float ConvertAnswerToFloat(string question, string answer)
        {
            Dictionary<string, Dictionary<string, int>> questionAnswerMappings = new Dictionary<string, Dictionary<string, int>>
            {
                //Happy Emotion Questions
               { "How often do you reward yourself with a special treat when you're happy?", new Dictionary<string, int> { { "Very often", 5 }, { "Sometimes", 2 }, { "Rarely", 3 }, { "Never", 5 } } },
               { "How likely are you to share a meal with others when you're happy?", new Dictionary<string, int> { { "Very likely", 2 }, { "Somewhat likely", 3 }, { "Not very likely", 12 }, { "Not at all likely", 8 } } },
               { "Would you prefer a small, satisfying dish or a large meal when happy?", new Dictionary<string, int> { { "Small, satisfying dish", 2 }, { "Large meal", 8 }, { "It depends", 7 }, { "I don't know", 14 } } },
               { "Do you tend to choose more indulgent options when you're feeling happy?", new Dictionary<string, int> { { "Yes, often", 7 }, { "Yes, sometimes", 3 }, { "Rarely", 6 }, { "Never", 2 } } },

      
               //Sad Emotion Questions
               { "Do you find comfort in food when you're feeling down?", new Dictionary<string, int> { { "Always", 7 }, { "Sometimes", 2 }, { "Rarely", 3 }, { "Never", 5 } } },
               { "Do you eat more or less when you're feeling sad?", new Dictionary<string, int> { { "More", 5 }, { "Less", 9 }, { "The same amount", 13 }, { "It depends", 7 } } },
               { "How likely are you to skip meals when you're sad?", new Dictionary<string, int> { { "Very likely", 13 }, { "Somewhat likely", 3 }, { "Not very likely", 9 }, { "Not at all likely", 12 } } },
               { "How often do you snack between meals when you're feeling sad?", new Dictionary<string, int> { { "Very often", 5 }, { "Sometimes", 9 }, { "Rarely", 6 }, { "Never", 2 } } },


                //Fear Emotion Questions
               { "How does fear or anxiety affect your appetite?", new Dictionary<string, int> { { "I eat more", 6 }, { "I eat less", 9 }, { "It doesn't affect my eating habits", 4 }, { "It depends", 1 } } },
               { "How often do you snack when feeling scared or anxious?", new Dictionary<string, int> { { "Always", 4 }, { "Sometimes", 14 }, { "Rarely", 10 }, { "Never", 1 } } },
               { "Do you tend to eat healthier or unhealthier when you're feeling scared or anxious?", new Dictionary<string, int> { { "Healthier", 1 }, { "Unhealthier", 15 }, { "It doesn't affect my food choices", 10 }, { "I don't know", 14 } } },
               { "Do you tend to eat alone or with others when you're scared or anxious?", new Dictionary<string, int> { { "Alone", 1 }, { "With others", 10 }, { "It doesn't matter", 4 }, { "I don't eat when I'm scared or anxious", 8 } } },


                //Anger Emotion Questions
               { "How often do you eat out of frustration or anger?", new Dictionary<string, int> { { "Always", 7 }, { "Sometimes", 2 }, { "Rarely", 3 }, { "Never", 5 } } },
               { "Do you tend to eat faster when you're angry?", new Dictionary<string, int> { { "Yes, always", 11 }, { "Yes, sometimes", 6 }, { "Rarely", 10 }, { "Never", 1 } } },
               { "When you're angry, do you prefer to eat alone or with others?", new Dictionary<string, int> { { "Alone", 5 }, { "With others", 11 }, { "It doesn't matter", 4 }, { "I don't eat when I'm angry", 6 } } },
               { "Do you opt for quick, easy meals when you're angry?", new Dictionary<string, int> { { "Yes, often", 7 }, { "Yes, sometimes", 3 }, { "Rarely", 6 }, { "Never", 2 } } },

            };

            if (questionAnswerMappings.TryGetValue(question, out var answerMappings))
            {
                if (answerMappings.TryGetValue(answer, out var numericalAnswer))
                {
                    return numericalAnswer;
                }
            }

            throw new ArgumentException($"Invalid question or answer: {question} - {answer}");
        }

        public string ConvertPredictionToLabel(float prediction)
        {
            switch (prediction)
            {
                case 1:
                    return "Desi Food";
                case 2:
                    return "Fast Food";
                // Add more cases as needed
                default:
                    return "Unknown Your Model give some error";
            }
        }

        public class MyModelOutput
        {
            public List<float> prediction { get; set; }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
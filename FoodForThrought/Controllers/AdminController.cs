using FoodForThrought.Data;
using FoodForThrought.Migrations;
using FoodForThrought.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Net;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace FoodForThrought.Controllers
{
    public class AdminController : Controller
    {
        public readonly ProductimageDbcontext _imageDbcontext;

        public readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ProductimageDbcontext imageDbcontext, IWebHostEnvironment webHostEnvironment)
        {
            _imageDbcontext = imageDbcontext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult AddProduct()
        {
            return View();
        }
        public IActionResult AdminDeshboard()
        {
            return View();
        }

        // Add Product In database
        [HttpPost]
        public IActionResult ReadImageFile(AddingProductModel product)
        {
            string uniqueFileName = null;

            if(product.product_img_NotMapped != null)
            {
                string ImageUploadFolder = Path.Combine
                    (_webHostEnvironment.WebRootPath, "images/Product_image");

                uniqueFileName = Guid.NewGuid().ToString() + "_" +
                    product.product_img_NotMapped.FileName;

                string filepath = Path.Combine(ImageUploadFolder, uniqueFileName);

                string fileDirectory = Path.GetDirectoryName(filepath);
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }


                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                   product.product_img_NotMapped.CopyTo(fileStream);
               }

                product.product_img = "~/wwwroot/images/Product_image";
                product.product_image_name = uniqueFileName;

                _imageDbcontext.Add(product);
                _imageDbcontext.SaveChanges();

                TempData["confirm"] = "Product Add Successfully ";

            }
            else
            {
                TempData["confirm"] = "Product Did Not Add Successfully ";
            }
            return RedirectToAction("AddProduct");
        }
    }
}

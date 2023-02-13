using FoodForThrought.Data;
using FoodForThrought.Migrations.ProductimageDbcontextMigrations;
using FoodForThrought.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;
using System.IO;

namespace FoodForThrought.Controllers
{
    public class ProductController : Controller
    {
        public readonly ProductimageDbcontext _imageDbcontext;
        
        public readonly ProductimageDbcontext _updateProductnow;

        public readonly ProductimageDbcontext _searchProductnow;

        public readonly ProductimageDbcontext _displayProductnow;

        public readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ProductimageDbcontext imageDbcontext, ProductimageDbcontext updateProductnow,
                                 ProductimageDbcontext searchProductnow, IWebHostEnvironment webHostEnvironment,
                                 ProductimageDbcontext displayProductnow)
        {
            _imageDbcontext = imageDbcontext;
            _updateProductnow = updateProductnow;
            _searchProductnow = searchProductnow;
            _displayProductnow = displayProductnow;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult AddProduct()
        {
            return View();
        } 
        
        public IActionResult DeleteProduct()
        {
            return View();
        }
        public IActionResult SearchandUpdateProduct()
        {
          var foods = _displayProductnow.AddingProduct.ToList();

            return View(foods);

        }
        public IActionResult ViewAllProduct()
        {
            return View();
        }

        // Add Product In database
        [HttpPost]
        public IActionResult AddProduct(AddingProductModel product)
        {

            string uniqueFileName = null;

            if (product.product_img_NotMapped != null)
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


        [HttpPost]
        public IActionResult UpdateProduct(AddingProductModel updateproduct)
        {/*

            string uniqueFileName = null;
            byte[] bytes = null;

            if (updateproduct.product_img_NotMapped != null)
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
                   

                using (Stream fileStream = updateproduct.product_img_NotMapped.OpenReadStream())
                {
                    using (BinaryReader br = new BinaryReader(fileStream))
                    {
                        bytes = br.ReadBytes((Int32)fileStream.Length);
                    }

                }

                updateproduct.product_img = Convert.ToBase64String(bytes,0,bytes.Length);

                _updateProductnow.Add(updateproduct);
                _updateProductnow.SaveChanges();

                TempData["confirm"] = "Product Add Successfully ";

            }
            else
            {
                TempData["confirm"] = "Product Did Not Add Successfully ";
            }
                */



            return RedirectToAction("AddProduct");
        }


        [HttpPost]
        public IActionResult SearchProduct(AddingProductModel Searchproduct)
        {
            string f_img_name = "";
            int f_id = 0;


            var Search_product = _searchProductnow.AddingProduct.ToList();

            if(Search_product != null)
            {
                foreach(var searchfood in Search_product)
                {
                    if(Searchproduct.Id == searchfood.Id)
                    {
                        // they save the name of the image file 
                        f_id = searchfood.Id;
                        f_img_name = searchfood.product_image_name;
                    }  
                    else
                    {
                        TempData["confirm"] = "Food not Found";
                    }                    
                }


                // Delete the image from the Folder and delete the Row From the database
                string fullPath = Path.Combine
                    (_webHostEnvironment.WebRootPath, "images/Product_image", f_img_name);


                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);

                    foreach (var searchfood in Search_product)
                    {
                        if (Searchproduct.Id == searchfood.Id)
                        {
                            _searchProductnow.Remove(searchfood);
                            _searchProductnow.SaveChanges();

                        }
                        else
                        {
                            TempData["confirm"] = "Food not Found";
                        }
                    }
                }
                else
                {
                    TempData["confirm"] = "Path has No Food";
                }
            }
            else
            {
                TempData["confirm"] = "Database has No Food";
            }

            // they save the name of the image file 


            /* string uniqueFileName = null;

             foreach (var searchfood in Search_product)
             {
                 if (Searchproduct.Id == searchfood.Id)
                 {
                     // Updating the product now
                     string ImageUploadFolder = Path.Combine
          (_webHostEnvironment.WebRootPath, "images/Product_image");

                     uniqueFileName = Guid.NewGuid().ToString() + "_" +
                         Searchproduct.product_img_NotMapped.FileName;

                     string filepath = Path.Combine(ImageUploadFolder, uniqueFileName);

                     string fileDirectory = Path.GetDirectoryName(filepath);
                     if (!Directory.Exists(fileDirectory))
                     {
                         Directory.CreateDirectory(fileDirectory);
                     }


                     using (var fileStream = new FileStream(filepath, FileMode.Create))
                     {
                         Searchproduct.product_img_NotMapped.CopyTo(fileStream);
                     }

                     Searchproduct.product_img = "~/wwwroot/images/Product_image";
                     Searchproduct.product_image_name = uniqueFileName;

                     _searchProductnow.Add(Searchproduct);
                     _searchProductnow.SaveChanges();

                     TempData["confirm"] = "Update Product Add Successfully ";

                 }
                 else
                 {
                     TempData["confirm"] = "Food not Found";
                 }
             }*/

            return RedirectToAction("SearchandUpdateProduct");
        }

        [HttpPost]
        public IActionResult Updateproduct(AddingProductModel Updateproduct)
        {

            var Updating_product = _updateProductnow.AddingProduct.ToList();

            if(Updating_product != null)
            {
                foreach(var productdata in Updating_product)
                {
                    string product_title = productdata.product_title;

                    if(Updateproduct.product_title == product_title)
                    {

                        string uniqueFileName = null;

                        if (Updateproduct.product_img_NotMapped != null)
                        {
                            string ImageUploadFolder = Path.Combine
                                (_webHostEnvironment.WebRootPath, "images/Product_image");

                            uniqueFileName = Guid.NewGuid().ToString() + "_" +
                                Updateproduct.product_img_NotMapped.FileName;

                            string filepath = Path.Combine(ImageUploadFolder, uniqueFileName);

                            string fileDirectory = Path.GetDirectoryName(filepath);
                            if (!Directory.Exists(fileDirectory))
                            {
                                Directory.CreateDirectory(fileDirectory);
                            }


                            using (var fileStream = new FileStream(filepath, FileMode.Create))
                            {
                                Updateproduct.product_img_NotMapped.CopyTo(fileStream);
                            }

                            Updateproduct.product_img = "~/wwwroot/images/Product_image";
                            Updateproduct.product_image_name = uniqueFileName;

                            _updateProductnow.Add(Updateproduct);
                            _updateProductnow.SaveChanges();

                            TempData["confirm"] = "Product Update Successfully ";

                        }
                        else
                        {
                            TempData["confirm"] = "Product Did Not Update Successfully ";
                        }


                    }
                }
            }
            else
            {

                TempData["confirm"] = "Product Data Not get From Database";
            }

            return RedirectToAction("UpdateProduct");
        }


    }
}

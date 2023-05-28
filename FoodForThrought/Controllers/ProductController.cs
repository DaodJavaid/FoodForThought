using FoodForThrought.Data;
using FoodForThrought.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;
using System.IO;

namespace FoodForThrought.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public readonly ProductimageDbcontext _imageDbcontext;
        
        public readonly ProductimageDbcontext _updateProductnow;

        public readonly ProductimageDbcontext _searchProductnow;

        public readonly ProductimageDbcontext _displayProductnow;

        public readonly ProductimageDbcontext _deleteProductnow;

        public readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ProductimageDbcontext imageDbcontext, ProductimageDbcontext updateProductnow,
                                 ProductimageDbcontext searchProductnow, IWebHostEnvironment webHostEnvironment,
                                 ProductimageDbcontext displayProductnow, ProductimageDbcontext deleteProductnow)
        {
            _imageDbcontext = imageDbcontext;
            _updateProductnow = updateProductnow;
            _searchProductnow = searchProductnow;
            _displayProductnow = displayProductnow;
            _webHostEnvironment = webHostEnvironment;
            _deleteProductnow = deleteProductnow;
        }

        public IActionResult AddProduct()
        {
            return View();
        } 
        
        public IActionResult DeleteProduct()
        {
            var foods = _displayProductnow.AddingProduct.ToList();

            return View(foods);
        }
        public IActionResult SearchandUpdateProduct(AddingProductModel delete)
        {
            var delete_product = _displayProductnow.AddingProduct.ToList();

            return View(delete_product);

        }
        public IActionResult ViewAllProduct()
        {
            var delete_product = _updateProductnow.AddingProduct.ToList();

            return View(delete_product);
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
        public IActionResult SearchProduct(AddingProductModel delete)
        {
            var delete_product = _updateProductnow.AddingProduct.ToList();

            DeletingProduct(delete);

            AddProduct(delete);

            TempData["confirm"] = "Product Update Successfully";

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

        [HttpPost]
        public IActionResult DeletingProduct(AddingProductModel delete)
        {

            var delete_product = _deleteProductnow.AddingProduct.ToList();

            if (delete_product != null)
            {
                foreach (var deletefood in delete_product)
                {
                    if (delete.product_title_old == deletefood.product_title)
                    {
                        // Delete image file from the server
                        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, deletefood.product_img, deletefood.product_image_name);

                        try
                        {
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle the exception (e.g. log it)
                            TempData["confirm"] = "Product Deleted have some issue";
                        }
                        // Remove product from the database
                        _deleteProductnow.Remove(deletefood);
                        _deleteProductnow.SaveChanges();

                        TempData["confirm"] = "Product Deleted Successfully";
                    }
                    else
                    {
                        TempData["confirm"] = "Product Not Found";
                    }
                }

            }
            return RedirectToAction("DeleteProduct");


        }
    }
}

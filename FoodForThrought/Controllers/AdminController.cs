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
       public IActionResult AdminDeshboard()
        {
            return View();
        }

    }
}

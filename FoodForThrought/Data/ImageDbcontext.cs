using FoodForThrought.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodForThrought.Data
{
    public class ImageDbcontext:DbContext
    {
        public ImageDbcontext(DbContextOptions<ImageDbcontext> options) : base(options) { 
        

        }
        public DbSet<AddProductModel> ProductImage { get; set; }
    }
}

using FoodForThrought.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodForThrought.Data
{
    public class ProductimageDbcontext:DbContext
    {
        public ProductimageDbcontext(DbContextOptions<ProductimageDbcontext> options) : base(options) { 
        

        }
        public DbSet<AddingProductModel> AddingProduct { get; set; }

        internal object Find(string product_title)
        {
            throw new NotImplementedException();
        }
    }
}

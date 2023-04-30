using FoodForThrought.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodForThrought.Data
{
    public class AdminDbContext: DbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
        {

        }

        public DbSet<AdminRegister1> AdminCheck { get; set; }
    }
}

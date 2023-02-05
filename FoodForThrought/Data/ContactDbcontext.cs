using FoodForThrought.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodForThrought.Data
{
    public class ContactDbcontext:DbContext
    {
        public ContactDbcontext(DbContextOptions<ContactDbcontext> options) : base(options)
        {

        }

        public DbSet<ContactUsForm> Message { get; set; }

    }
}

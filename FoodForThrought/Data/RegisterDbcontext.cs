using Microsoft.EntityFrameworkCore;
using FoodForThrought.Models;

namespace FoodForThrought.Data
{
    public class RegisterDbcontext : DbContext
    {
        public RegisterDbcontext(DbContextOptions<RegisterDbcontext> options):base(options)
        {
            
        }

        public DbSet<AdminRegister> Signup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminRegister>()
                .Property(r => r.address)
                .IsRequired(false);

            modelBuilder.Entity<AdminRegister>()
                .Property(r => r.city)
                .IsRequired(false);

            modelBuilder.Entity<AdminRegister>()
              .Property(r => r.gender)
              .IsRequired(false);

            modelBuilder.Entity<AdminRegister>()
              .Property(r => r.country)
              .IsRequired(false);

            modelBuilder.Entity<AdminRegister>()
              .Property(r => r.zip)
              .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}

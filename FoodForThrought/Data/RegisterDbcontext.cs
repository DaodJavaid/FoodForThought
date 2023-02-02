﻿using Microsoft.EntityFrameworkCore;
using FoodForThrought.Models;

namespace FoodForThrought.Data
{
    public class RegisterDbcontext:DbContext
    {
        public RegisterDbcontext(DbContextOptions<RegisterDbcontext> options):base(options)
        {
            
        }

        public DbSet<Register> Signup { get; set; }
    }
}

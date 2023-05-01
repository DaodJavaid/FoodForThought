using FoodForThrought.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodForThrought.Data
{
    public class QuestionnaireDbContext : DbContext
    {
        public QuestionnaireDbContext(DbContextOptions<QuestionnaireDbContext> options) : base(options)
        {

        }

        public DbSet<QuestionnaireModel> Question { get; set; }
    }
}

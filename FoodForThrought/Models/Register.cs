using System.ComponentModel.DataAnnotations;

namespace FoodForThrought.Models
{
    public class AdminRegister
    {
        [Key]
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
    }
}

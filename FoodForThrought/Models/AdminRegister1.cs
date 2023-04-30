using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodForThrought.Models
{

    public class AdminRegister1
    {
        [Key]
        public string admin_username { get; set; }
        public string admin_email { get; set; }
        public string admin_password { get; set; }
        public string admin_confirm_password { get; set; }
    }
}

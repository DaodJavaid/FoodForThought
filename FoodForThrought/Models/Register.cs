using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodForThrought.Models
{
    public class AdminRegister
    {
        [Key]
        public int Id { get; set; }
        public string username { get; set; }
        [Required]
        public string email { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
        [NotMapped]
        public string email_old { get; set; }

    }
}

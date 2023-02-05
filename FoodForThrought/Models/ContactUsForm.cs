using System.ComponentModel.DataAnnotations;

namespace FoodForThrought.Models
{
    public class ContactUsForm
    {
        public string user_name { get; set; }
        [Key]
        public string user_email { get; set; }
        public string user_message { get; set; }

    }
}

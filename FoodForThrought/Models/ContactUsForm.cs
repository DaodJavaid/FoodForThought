using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodForThrought.Models
{
    public class ContactUsForm
    {
        public string user_name { get; set; }
        [Key]
        public string user_email { get; set; }
        public string user_message { get; set; }
        [NotMapped]
        public string old_user_email { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace FoodForThrought.Models
{
    public class AddProductModel
    {
        [Key]
        public int product_name { get; set; }
        public string product_des { get; set; }
        public string product_img { get; set; }
    }
}

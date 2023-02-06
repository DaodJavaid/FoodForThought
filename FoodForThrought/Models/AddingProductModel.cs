using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodForThrought.Models
{
    public class AddingProductModel
    {
        [Key]
        public string product_title { get; set; }
        public string product_desription { get; set; }
        public string product_img { get; set; }
        
        [NotMapped]
        public IFormFile product_img_NotMapped { get; set; }


    }
}

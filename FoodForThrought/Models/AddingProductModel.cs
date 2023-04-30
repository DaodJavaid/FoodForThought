using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace FoodForThrought.Models
{
    public class AddingProductModel
    {
        [Key]
        public int Id { get; set; }
        public string product_title { get; set; }
        public string product_desription { get; set; }
        public string product_img { get; set; }
        public string product_image_name { get; set; }
        [NotMapped]
        public IFormFile product_img_NotMapped { get; set; }
        [NotMapped]
        public string product_title_old { get; set; }

    }
}

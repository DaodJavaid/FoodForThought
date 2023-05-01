using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodForThrought.Models
{
    public class QuestionnaireModel
    {
        [Key]
        public int Id { get; set; }
        public string emotion_questtion { get; set; }
        public string first_option { get; set; }
        public string second_option { get; set; }
        public string third_option { get; set; }
        public string forth_option { get; set; }
        [NotMapped]
        public int old_id { get; set; }
    }
}

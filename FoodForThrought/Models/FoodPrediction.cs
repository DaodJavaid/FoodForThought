using System.ComponentModel.DataAnnotations.Schema;

namespace FoodForThrought.Models
{
    public class FoodPrediction
    {
        public string question1 { get; set; }
        public string question2 { get; set; }
        public string question3 { get; set; }
        public string question4 { get; set; }

        [NotMapped]
        public string Fullquestion1 { get; set; }
        [NotMapped]
        public string Fullquestion2 { get; set; }
        [NotMapped]
        public string Fullquestion3 { get; set; }
        [NotMapped]
        public string Fullquestion4 { get; set; }
    }
}

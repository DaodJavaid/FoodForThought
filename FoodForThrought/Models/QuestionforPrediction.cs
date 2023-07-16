using Microsoft.ML.Data;

namespace FoodForThrought.Models
{
    public class QuestionforPrediction
    {
        [ColumnName("question1"), LoadColumn(0)]
        public float Question1 { get; set; }

        [ColumnName("question2"), LoadColumn(1)]
        public float Question2 { get; set; }

        [ColumnName("question3"), LoadColumn(2)]
        public float Question3 { get; set; }

        [ColumnName("question4"), LoadColumn(3)]
        public float Question4 { get; set; }
    }
}

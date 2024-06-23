namespace Swole.Models
{
    public class ExerciseRecord
    {
        public int Record_Id { get; set; }
        public int Exercise_Id { get; set; }
        public DateTime Date_Recorded { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal? Weight { get; set; }
        public int? Duration_Minutes { get; set; }
        public string Notes { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swole.Models
{
    [Table("exercises")]
    public class Exercise
    {
        [Key]
        public int Exercise_Id { get; set; }

        public string Exercise_Name { get; set; }

        public int Category_Id { get; set; }

        // Navigation property to exercise category
        [ForeignKey("Category_Id")]
        public ExerciseCategory ExerciseCategory { get; set; }

        // Navigation property to exercise records
        public ICollection<ExerciseRecord> ExerciseRecords { get; set; }
    }
}

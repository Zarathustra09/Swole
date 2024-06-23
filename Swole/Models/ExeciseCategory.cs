using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Swole.Models
{
    [Table("exercise_categories")]
    public class ExerciseCategory
    {
        [Key]
        public int Category_Id { get; set; }

        public string Category_Name { get; set; }

        // Navigation property to exercises
        public ICollection<Exercise> Exercises { get; set; }
    }
}

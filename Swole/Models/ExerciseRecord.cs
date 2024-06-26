﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swole.Models
{
    [Table("exercise_records")]
    public class ExerciseRecord
    {
        [Key]
        public int Record_Id { get; set; }

        public int Exercise_Id { get; set; }

        public DateTime Date_Recorded { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public decimal? Weight { get; set; }

        public int? Duration_Minutes { get; set; }

        public string Notes { get; set; }

        // Navigation property to exercise
        [ForeignKey("Exercise_Id")]
        public Exercise Exercise { get; set; }
    }
}

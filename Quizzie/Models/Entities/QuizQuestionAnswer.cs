namespace Quizzie.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuizQuestionAnswer")]
    public partial class QuizQuestionAnswer
    {
        public int ID { get; set; }

        [Required]
        public string Answer { get; set; }

        public bool IsCorrect { get; set; }

        public int QuizQuestionID { get; set; }

        public virtual QuizQuestion QuizQuestion { get; set; }
    }
}

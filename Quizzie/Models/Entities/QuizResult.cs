namespace Quizzie.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuizResult")]
    public partial class QuizResult
    {
        public int ID { get; set; }

        public string Result { get; set; }

        public DateTime? DateCompleted { get; set; }

        public int QuizID { get; set; }

        public virtual Quiz Quiz { get; set; }

        public virtual QuizResult QuizResult1 { get; set; }

        public virtual QuizResult QuizResult2 { get; set; }
    }
}

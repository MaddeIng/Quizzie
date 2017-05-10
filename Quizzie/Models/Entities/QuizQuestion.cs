namespace Quizzie.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuizQuestion")]
    public partial class QuizQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public QuizQuestion()
        {
            QuizQuestionAnswers = new HashSet<QuizQuestionAnswer>();
        }

        public int ID { get; set; }

        [Required]
        public string Question { get; set; }

        [StringLength(50)]
        public string ImageLink { get; set; }

        public int QuizID { get; set; }

        public virtual Quiz Quiz { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuizQuestionAnswer> QuizQuestionAnswers { get; set; }
    }
}

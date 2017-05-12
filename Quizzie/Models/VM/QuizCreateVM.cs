using Quizzie.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quizzie.Models.VM
{
    public class QuizCreateVM
    {
        [Required]
        public string CreateTitle { get; set; }
               
        public QuizQuestion CreateQuestion { get; set; }

        [Required]
        public List<QuizQuestionAnswer> CreateAnswers { get; set; }
    }
}
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
        [Required(ErrorMessage = "Ange en titel")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Skriv in en fråga")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Skriv in svar")]
        public string Answer1 { get; set; }

        [Required(ErrorMessage = "Skriv in svar")]
        public string Answer2 { get; set; }

        [Required(ErrorMessage = "Skriv in svar")]
        public string Answer3 { get; set; }

        [Required(ErrorMessage = "Skriv in svar")]
        public string Answer4 { get; set; }

        public string RadioAnswer { get; set; }

        public int AccessCode { get; set; } = 0;
    }
}
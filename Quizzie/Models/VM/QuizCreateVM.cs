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

        //[Required(ErrorMessage = "Ange en titel")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Skriv in en fråga")]
        [StringLength(50, ErrorMessage = "Max 50 characters!")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Skriv in svar")]
        [StringLength(20,ErrorMessage = "Max 20 characters!")]
        public string Answer1 { get; set; }

        [Required(ErrorMessage = "Skriv in svar")]
        [StringLength(20, ErrorMessage = "Max 20 characters!")]
        public string Answer2 { get; set; }

        [Required(ErrorMessage = "Skriv in svar")]
        [StringLength(20, ErrorMessage = "Max 20 characters!")]
        public string Answer3 { get; set; }

        [Required(ErrorMessage = "Skriv in svar")]
        [StringLength(20, ErrorMessage = "Max 20 characters!")]
        public string Answer4 { get; set; }

        public string RadioAnswer { get; set; }

        public int AccessCode { get; set; }
    }
}
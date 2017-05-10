using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.VM
{
    public class QuizQuestionVM
    {
        public string Question { get; set; }

        public List<string> Answers { get; set; }

    }
}
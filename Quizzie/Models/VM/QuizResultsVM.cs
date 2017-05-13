using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.VM
{
    public class QuizResultsVM
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public int TotalPoints { get; set; }
    }
}
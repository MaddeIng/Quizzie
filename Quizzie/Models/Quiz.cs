using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.Entities
{
    public partial class Quiz
    {

        public static void AddQuiz()
        {
            QuizzieDBContext context = new QuizzieDBContext();

            var quiz = new Quiz
            {
                Title = "testquizz",
                AccessCode = 9999,
                CreatedBy = 0,                
            };

            context.Quizs.Add(quiz);

            var result = context.SaveChanges();
        }

    }
}
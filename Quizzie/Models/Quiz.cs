using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.Entities
{
    public partial class Quiz
    {

        public static Quiz GetQuiz()
        {
            int id = 3;

            var context = new QuizzieDBContext();

            var result = context.Quizs
                .SingleOrDefault(q => q.ID == id);

            return result;
        }

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.Entities
{
    public partial class QuizQuestion
    {
        public static void AddQuizQuestion()
        {
            QuizzieDBContext context = new QuizzieDBContext();

            var quizQuestion = new QuizQuestion
            {
                QuizID = 3,
                Question = "Vad är Gustav Dalén känd för ?",
                ImageLink = "/img/img0_0.jpg",
            };

            context.QuizQuestions.Add(quizQuestion);

            var result = context.SaveChanges();

        }
    }
}
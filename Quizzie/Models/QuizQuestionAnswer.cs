using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.Entities
{
    public partial class QuizQuestionAnswer
    {
        public static void AddQuizQuestionAnswer()
        {
            QuizzieDBContext context = new QuizzieDBContext();

            var quizQuestionAnswers = new List<QuizQuestionAnswer>()
            {
                new QuizQuestionAnswer {QuizQuestionID = 2, Answer = "Från TV-serien Rederiet.", IsCorrect = false },
                new QuizQuestionAnswer {QuizQuestionID = 2, Answer = "Uppfinnaren bakom AGA fyren.", IsCorrect = true },
                new QuizQuestionAnswer {QuizQuestionID = 2, Answer = "Olof Palmes mördare", IsCorrect = false },
                new QuizQuestionAnswer {QuizQuestionID = 2, Answer = "Olof Palmes \"bästa\" vän.", IsCorrect = false },
            };
            foreach (var qqa in quizQuestionAnswers)
            {
                context.QuizQuestionAnswers.Add(qqa);
            }

            var result = context.SaveChanges();
        }
    }
}
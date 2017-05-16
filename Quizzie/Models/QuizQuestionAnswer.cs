using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.Entities
{
    public partial class QuizQuestionAnswer
    {
        public static void AddQuizQuestionAnswer(List<QuizQuestionAnswer> quizQuestionAnswers)
        {
            QuizzieDBContext context = new QuizzieDBContext();

            //var quizQuestionAnswers = new List<QuizQuestionAnswer>()
            //{
            //    //new QuizQuestionAnswer {QuizQuestionID = 6, Answer = "Från TV-serien Rederiet.", IsCorrect = false },
            //    //new QuizQuestionAnswer {QuizQuestionID = 6, Answer = "Uppfinnaren bakom AGA fyren.", IsCorrect = false },
            //    //new QuizQuestionAnswer {QuizQuestionID = 6, Answer = "Hockeylegend", IsCorrect = true },
            //    //new QuizQuestionAnswer {QuizQuestionID = 6, Answer = "En smart kille", IsCorrect = false },

            //    //new QuizQuestionAnswer {QuizQuestionID = 3, Answer = "Från TV-serien Rederiet", IsCorrect = true},
            //    //new QuizQuestionAnswer {QuizQuestionID = 3, Answer = "Uppfinnaren bakom AGA fyren", IsCorrect = false},
            //    //new QuizQuestionAnswer {QuizQuestionID = 3, Answer = "Framgångsrik hockeyspelare", IsCorrect = false },
            //    //new QuizQuestionAnswer {QuizQuestionID = 3, Answer = "Olof Palmes \"bästa\" vän", IsCorrect = false },
            //};

            foreach (var qqa in quizQuestionAnswers)
            {
                context.QuizQuestionAnswers.Add(qqa);
            }

            var result = context.SaveChanges();
        }
    }
}
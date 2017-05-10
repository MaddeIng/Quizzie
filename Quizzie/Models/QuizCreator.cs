using Quizzie.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.Entities
{
    public partial class QuizCreator
    {
        public static void AddQuizCreator()
        {
            QuizzieDBContext context = new QuizzieDBContext();

            var quizCreator = new QuizCreator
            {
                UserName = "fantomen",
                email = "acme@acme.com",
                password = "fantomen"
            };

            context.QuizCreators.Add(quizCreator);

            var result = context.SaveChanges();

        }

    }
}
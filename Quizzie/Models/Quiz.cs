using Newtonsoft.Json;
using Quizzie.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.Entities
{
    public partial class Quiz
    {
        static QuizzieDBContext context;

        //public static Quiz GetQuiz()
        //{
        //    int id = 3;

        //    var context = new QuizzieDBContext();

        //    var result = context.Quizs
        //        .SingleOrDefault(q => q.ID == id);

        //    return result;
        //}

        public static void AddQuiz(QuizCreateVM viewModel)
        {
            context = new QuizzieDBContext();

            var quiz = new Quiz
            {
                Title = viewModel.Title,
                AccessCode = 9999,
                CreatedBy = 0,
            };

            context.Quizs.Add(quiz);

            var result = context.SaveChanges();
        }

        public static int RandomizedQuizCode()
        {
            Random rnd = new Random();
            int randomCode = 0;

            // Kollar om accesskod redan finns i databasen, genererar ny om så är fallet. 
            if (context.Quizs.Any(q => q.AccessCode == randomCode)) 
            {
                randomCode = rnd.Next(1000, 10000); // random code between 1000 and 9999.
            }

            return randomCode;
        }

    }
}
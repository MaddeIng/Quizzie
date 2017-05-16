using Newtonsoft.Json;
using Quizzie.Models.VM;
using Quizzie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.Entities
{
    public partial class Quiz
    {
        static QuizzieDBContext context = new QuizzieDBContext();

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
            var quiz = new Quiz
            {
                Title = viewModel.Title,
                AccessCode = RandomizedQuizCode(),
                CreatedBy = 0,
            };

            context.Quizs.Add(quiz);
            context.SaveChanges();

            QuizQuestion question = new QuizQuestion();
            List<QuizQuestionAnswer> answers = new List<QuizQuestionAnswer>();

            quiz.ID = context.Quizs.FirstOrDefault(q => q.AccessCode == quiz.AccessCode).ID;

            QuizQuestion.AddQuizQuestion(quiz.ID, viewModel.Question);

            //QuizQuestionAnswer answer = new QuizQuestionAnswer() { QuizQuestionID = 6, Answer = "Från TV-serien Rederiet.", IsCorrect = false };

            answers.Add()

            question.QuizID = quiz.ID;
            question.Question = viewModel.Question;
            question.QuizQuestionAnswers = answers;
            question.ImageLink = $"img{quiz.ID}_{question.ID}.jpg";

            context.QuizQuestions.Add(question);
            context.SaveChanges();


        }

        public static int RandomizedQuizCode()
        {
            Random rnd = new Random();
            int randomCode = 0;

            randomCode = rnd.Next(1000, 10000);

            // Kollar om accesskod redan finns i databasen, genererar ny om så är fallet. 
            while (context.Quizs.Any(q => q.AccessCode == randomCode)) 
                randomCode = rnd.Next(1000, 10000); // random code between 1000 and 9999.

            return randomCode;
        }

    }
}
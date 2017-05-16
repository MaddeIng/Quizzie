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

        public static int NewQuiz()
        {
            var quiz = new Quiz
            {
                Title = "Ett nytt quiz",
                AccessCode = RandomizedQuizCode(),
                CreatedBy = 0,
            };

            context.Quizs.Add(quiz);
            context.SaveChanges();
            return quiz.ID;
        }

        public static void AddQuestion(QuizCreateVM viewModel, int quizId, int questionId)
        {
            //var quiz = context.Quizs.Find(quizId);

            int questionID = QuizQuestion.AddQuizQuestion(quizId, viewModel.Question);

            List<QuizQuestionAnswer> answers = new List<QuizQuestionAnswer>();

            int correct = Convert.ToInt32(viewModel.RadioAnswer);

            answers.Add(new QuizQuestionAnswer() { QuizQuestionID = questionID, Answer = viewModel.Answer1, IsCorrect = false });
            answers.Add(new QuizQuestionAnswer() { QuizQuestionID = questionID, Answer = viewModel.Answer2, IsCorrect = false });
            answers.Add(new QuizQuestionAnswer() { QuizQuestionID = questionID, Answer = viewModel.Answer3, IsCorrect = false });
            answers.Add(new QuizQuestionAnswer() { QuizQuestionID = questionID, Answer = viewModel.Answer4, IsCorrect = false });

            answers[correct-1].IsCorrect = true;

            QuizQuestionAnswer.AddQuizQuestionAnswer(answers);

        }

        internal static QuizCreateVM GetQuestion(int quizId, int questionId)
        {
            // Sätt proppar från DB (om träff på frågan)
            return new QuizCreateVM
            {
            };
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
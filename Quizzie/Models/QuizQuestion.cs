using Quizzie.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.Entities
{
    public partial class QuizQuestion
    {
        /// <summary>
        /// Adds a hard-coded question to the Db.
        /// </summary>
        public static void AddQuizQuestion()
        {
            QuizzieDBContext context = new QuizzieDBContext();

            var quizQuestion = new QuizQuestion
            {
                //QuizID = 3,
                //Question = "Vad är Gustav Dalén känd för ?",
                //ImageLink = "/img/img3_2.jpg",
                //QuizID = 3,               
                //Question = "Vad är Mikael Dahlén känd för?",
                //ImageLink = "/img/img3_7.jpg",
            };

            context.QuizQuestions.Add(quizQuestion);

            var result = context.SaveChanges();

        }
        

        public static QuizQuestionVM GetQuestionViewModel(int id)
        {
            QuizQuestionVM myViewModel = new QuizQuestionVM();
            QuizzieDBContext qDBcontext = new QuizzieDBContext();   //DataBasen

            var question = qDBcontext.QuizQuestions
                .FirstOrDefault(q => q.ID == id);


            var answers = qDBcontext.QuizQuestionAnswers
                .Where(a => a.QuizQuestionID == id).ToList();


            myViewModel.Question = question;
            myViewModel.Answers = answers;

            return myViewModel;
        }
    }
}
using Quizzie.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie.Models.Entities
{
    public partial class QuizQuestion
    {
        public static int AddQuizQuestion(int quizID, string question)
        {
            QuizzieDBContext context = new QuizzieDBContext();

            var quizQuestion = new QuizQuestion
            {
                QuizID = quizID,
                Question = question
            };

            context.QuizQuestions.Add(quizQuestion);
            context.SaveChanges();

            int questionID = context.QuizQuestions.Where(q => q.QuizID == quizID).FirstOrDefault(p => p.Question == question).ID;

            quizQuestion.ImageLink = $"/img/img{quizID}_{questionID}.jpg";
            context.SaveChanges();

            return questionID;
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
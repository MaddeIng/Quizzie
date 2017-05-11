using Microsoft.AspNet.SignalR;
using Quizzie.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie
{
    public class QuizHub : Hub
    {
        public void PushQuiz()
        {
            var quiz = Quiz.GetQuiz();
            Clients.All.pushQuiz(quiz);
        }

        public bool IsCorrect(int quizQuestionAnswerID)
        {
            QuizzieDBContext context = new QuizzieDBContext();

            var isCorrect = context.QuizQuestionAnswers
                .SingleOrDefault(a => a.ID == quizQuestionAnswerID)
                .IsCorrect;                

            //Clients.Caller.isCorrect(isCorrect);
            return isCorrect;
        }

    }
}
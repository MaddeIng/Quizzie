using Microsoft.AspNet.SignalR;
using Quizzie.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie
{
    public class QuizCreateHub : Hub
    {
        static QuizzieDBContext context = new QuizzieDBContext();

        public void GetQuestionDetails(string title, string question, string[] answers, string correctAnswer)
        {
            List<Quiz> newQuiz = new List<Quiz>();
            
            string tit = title;
            string qstn = question;
            string[] ans = answers;
            string corr = correctAnswer;



        }


    }
}
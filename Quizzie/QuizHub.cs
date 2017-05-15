using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Quizzie.Models.Entities;
using Quizzie.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Quizzie
{
    public class QuizHub : Hub
    {

        static QuizzieDBContext context = new QuizzieDBContext();

        public bool ValidateStartOfQuiz(string name, string _accessCode)
        {
            Console.WriteLine(Context.ConnectionId);
            bool isValid = false;
            int accessCode = 0;
            try
            { accessCode = Convert.ToInt32(_accessCode); }
            catch (Exception)
            {
                return isValid;
            }

            isValid = context.Quizs
                .Any(q => q.AccessCode == accessCode);
            if(isValid)
            {
                Clients.Caller.Name = name;
                Clients.Caller.AccessCode = accessCode;
                Clients.Caller.CurrentQuestion = 0;
                Clients.Caller.Points = 0;
            }
            return isValid;
        }

        public void Initialize(string accessCode)
        {           

            Groups.Add(Context.ConnectionId, accessCode);
            //Thread.Sleep(2000);
            Clients.Group(accessCode).addChatMessage("hello "+Clients.Caller.Name + " join the game");



            var code = 0;
            try
            { code = Convert.ToInt32(Clients.Caller.AccessCode); }
            catch
            { Console.WriteLine("Error parson accesCode in GetQuestionFromId"); }


            var quizID = context
                .Quizs
                .SingleOrDefault(a => a.AccessCode == code).ID;


            var noOfQuestions = context
                .QuizQuestions.Where(a => a.QuizID == quizID)
                .Select(q => q.ID)
                .ToList()             
                .Count();

            Clients.Caller.QuizID = quizID;
            Clients.Caller.NoOfQuestions = noOfQuestions;

            var quizQuestionID = GetQuestionFromId((int)Clients.Caller.CurrentQuestion, quizID);

            //Get the question
            var question = QuizQuestion.GetQuestionViewModel(quizQuestionID);

            SetQuestion(Clients.Caller, question);
        }

        private static int GetQuestionFromId(int questionID, int quizID)
        {            
            var result = context
                .QuizQuestions.Where(a => a.QuizID == quizID)
                .Select(q => q.ID)
                .ToList()
                .ElementAt(questionID);

            return result;
        }

        public bool IsCorrect(int quizQuestionAnswerID)
        {

            var isCorrect = context.QuizQuestionAnswers
                .SingleOrDefault(a => a.ID == quizQuestionAnswerID)
                .IsCorrect;

            GoToNextQuestion(isCorrect);

            return isCorrect;
        }

        private void GoToNextQuestion(bool isCorrect)
        {
            var quizQuestionID = 0;
            int noOfQuestions = 0;
            try
            {
                noOfQuestions = Convert.ToInt32(Clients.Caller.NoOfQuestions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (Clients.Caller.CurrentQuestion < noOfQuestions - 1)
            {
                Clients.Caller.CurrentQuestion++;
                quizQuestionID = GetQuestionFromId((int)Clients.Caller.CurrentQuestion, (int)Clients.Caller.QuizID);

                DelayedChangeQuestion(Clients.Caller, QuizQuestion.GetQuestionViewModel(quizQuestionID));

                // Method to count points
                int points = CalculatePoints(Clients.Caller, isCorrect);
            }
            else if (Clients.Caller.CurrentQuestion == noOfQuestions - 1)
            {
               var finished = QuizLengthFinished();
                Clients.Caller.quizLengthFinished(finished);

            }

        }
        private string QuizLengthFinished()
        {

            return "Färdig";
        }

        private int CalculatePoints(dynamic caller, bool isCorrect)
        {
            if (isCorrect == true)
            {
                caller.Points++;
            }
            return (int)caller.Points;
        }

        private void SetQuestion(dynamic target, QuizQuestionVM question)
        {
            // Make the answers serializable
            var answers = question.Answers.Select(a => new { Answer = a.Answer, ID = a.ID }).ToList();

            var _question = new { Question = question.Question?.Question, ImageLink = question.Question?.ImageLink };

            // Send the answers to the caller of this function
            Clients.Caller.setQuestion(_question, answers);
            //Clients.Caller.showBodyElement();
        }

        private void DelayedChangeQuestion(dynamic target, QuizQuestionVM question)
        {
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((obj) =>
            {
                SetQuestion(target, question);
                timer.Dispose();
            }, null, 1000, System.Threading.Timeout.Infinite);            
        }

    }
}
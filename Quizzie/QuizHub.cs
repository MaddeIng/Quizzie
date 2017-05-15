using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Quizzie.Models.Entities;
using Quizzie.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie
{
    public class QuizHub : Hub
    {

        static int noOfQuestions;
        static QuizzieDBContext context = new QuizzieDBContext();

        public bool ValidateStartOfQuiz(string name, string _accessCode)
        {
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

            return isValid;
        }

        public void Initialize(string name, string accessCode)
        {
            Clients.Caller.Name = name;
            Clients.Caller.CurrentQuestion = 0;
            Clients.Caller.Points = 0;

            noOfQuestions = context.QuizQuestions.Count();

            var quizQuestionID = GetQuestionFromId((int)Clients.Caller.CurrentQuestion);

            //Get the question
            var question = QuizQuestion.GetQuestionViewModel(quizQuestionID);

            SetQuestion(Clients.Caller, question);
        }

        private static int GetQuestionFromId(int questionID)
        {
            var result = context
                .QuizQuestions.Select(q => q.ID)
                .ToList()
                .ElementAt(questionID);

            return result;
        }

        public string GetQuiz()
        {
            var quiz = Quiz.GetQuiz();

            string title = quiz.Title;

            var questions = QuizQuestion.GetQuestionViewModel(2);

            questions.QuizTitle = title;

            var json = JsonConvert.SerializeObject(questions);
            return json;
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

            if (Clients.Caller.CurrentQuestion < noOfQuestions - 1)
            {
                Clients.Caller.CurrentQuestion++;
                quizQuestionID = GetQuestionFromId((int)Clients.Caller.CurrentQuestion);

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
            Clients.Caller.showBodyElement();
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
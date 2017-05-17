using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Quizzie.Models.Entities;
using Quizzie.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Quizzie
{

    public class FinalResult
    {
        public string Name { get; set; }
        public string Score { get; set; }
    }

    public class QuizHub : Hub
    {
        static string adminConnection = "";
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
            if (isValid)
            {
                Clients.Caller.Name = name;
                Clients.Caller.AccessCode = accessCode;
                Clients.Caller.CurrentQuestion = 0;
                Clients.Caller.Score = 0;

                if (Clients.Caller.Name == "Admin")
                {
                    adminConnection = Context.ConnectionId;
                }
            }
            return isValid;
        }

        public void Initialize(string accessCode)
        {
            Groups.Add(Context.ConnectionId, accessCode);

            Clients.Group(accessCode).addChatMessage("hello " + Clients.Caller.Name + " join the game");
            if (Clients.Caller.Name != "Admin")
            {
                Clients.Client(adminConnection).showUsers(Clients.Caller.Name + " ");

            }

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
            if (Clients.Caller.Name != "Admin")
            {
                Clients.Client(adminConnection).changeAppearance(Clients.Caller.Name);
            }

            var isCorrect = context.QuizQuestionAnswers
                .SingleOrDefault(a => a.ID == quizQuestionAnswerID)
                .IsCorrect;

            if (isCorrect)
            {
                Clients.Caller.Score++;
            }

            if (Clients.Caller.Name == "Admin")
            {
                GoToNextQuestion();
            }

            return isCorrect;
        }

        private void GoToNextQuestion()
        {
            var quizQuestionID = 0;
            int noOfQuestions = 0;


            Clients.Client(adminConnection).removeAppearance();


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
                string group = Clients.Caller.AccessCode.ToString();

                quizQuestionID = GetQuestionFromId((int)Clients.Caller.CurrentQuestion, (int)Clients.Caller.QuizID);



                DelayedChangeQuestion(Clients.Group(group), QuizQuestion.GetQuestionViewModel(quizQuestionID));
            }
            else if (Clients.Caller.CurrentQuestion == noOfQuestions - 1)
            {
                var finished = QuizLengthFinished();
            }
        }

        private string QuizLengthFinished()
        {
            string group = Clients.Caller.AccessCode.ToString();

            Clients.Group(group).CalculateFinalScore();
            return "Finished!";
        }

        public FinalResult CalculateIndividualScore()
        {
            var score = Clients.Caller.Score.ToString();
            var name = Clients.Caller.Name;

            FinalResult _result = new FinalResult { Name = name, Score = score };

            string accessCode = Clients.Caller.AccessCode.ToString();

            if (Clients.Caller.Name != "Admin")
            {
            Clients.Group(accessCode).addChatMessage(Clients.Caller.Name + Clients.Caller.Score);

            Clients.Group(accessCode).justDoIt(_result);
            }

            return _result;
        }

        private void SetQuestion(dynamic target, QuizQuestionVM question)
        {
            // Make the answers serializable
            var answers = question.Answers.Select(a => new { Answer = a.Answer, ID = a.ID }).ToList();

            var _question = new { Question = question.Question?.Question, ImageLink = question.Question?.ImageLink };

            // Send the answers to the caller of this function
            string group = Clients.Caller.AccessCode.ToString();
            Clients.Group(group).setQuestion(_question, answers);
        }

        private void DelayedChangeQuestion(dynamic target, QuizQuestionVM question)
        {
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((obj) =>
            {
                SetQuestion(target, question);
                timer.Dispose();
            }, null, 1, System.Threading.Timeout.Infinite);
        }

    }
}
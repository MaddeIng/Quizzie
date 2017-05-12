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

        static int currentQuestion = 0;
        static int questionID;
        static int noOfQuestions;
        static QuizzieDBContext context = new QuizzieDBContext();

        public void Initialize()
        {
            noOfQuestions = context.QuizQuestions.Count();
            GetCurrentQuestionId();

            //Get the question
            var question = QuizQuestion.GetQuestionViewModel(questionID);

            SetQuestion(Clients.Caller, question);
        }

        private static void GetCurrentQuestionId()
        {
            questionID = context
                .QuizQuestions.Select(q => q.ID)
                .ToList()
                .ElementAt(currentQuestion);
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
            QuizzieDBContext context = new QuizzieDBContext();

            var isCorrect = context.QuizQuestionAnswers
                .SingleOrDefault(a => a.ID == quizQuestionAnswerID)
                .IsCorrect;

            if (currentQuestion < noOfQuestions-1)
            {
                currentQuestion++;
                GetCurrentQuestionId();
            }

            DelayedChangeQuestion(Clients.Caller, QuizQuestion.GetQuestionViewModel(questionID));

            return isCorrect;
        }


        private void SetQuestion(dynamic target, QuizQuestionVM question)
        {
            // Make the answers serializable
            var answers = question.Answers.Select(a => new { Answer = a.Answer, ID = a.ID }).ToList();

            var _question = new { Question = question.Question.Question, ImageLink = question.Question.ImageLink };

            // Send the answers to the caller of this function
            Clients.Caller.setQuestion(_question, answers);
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
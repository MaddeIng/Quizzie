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
        static int questionId;

        public void Initialize()
        {
            QuizzieDBContext context = new QuizzieDBContext();

            questionId = context
                .QuizQuestions.Select(q => q.ID)
                .ToList()
                .ElementAt(currentQuestion);
            
            //Get the question
            var question = QuizQuestion.GetQuestionViewModel(questionId);

            SetQuestion(Clients.Caller, question);
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

            currentQuestion++;
            DelayedChangeQuestion(Clients.Caller, QuizQuestion.GetQuestionViewModel(questionId));

            return isCorrect;
        }


        private void SetQuestion(dynamic target, QuizQuestionVM question)
        {
            // Make the answers serializable
            var answers = question.Answers.Select(a => new { Answer = a.Answer, ID = a.ID }).ToList();

            var _question = new { Question = question.Question.Question, ImageLink = question.Question.ImageLink};

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
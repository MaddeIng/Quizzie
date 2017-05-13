using Quizzie.Models;
using Quizzie.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quizzie.Controllers
{
    public class QuizController : Controller
    {
        [Route("Quiz/Index")]
        public ActionResult Index()
        {
            //Metoder som lägger till hårdkodade data i datbasen (för test)
            //QuizCreator.AddQuizCreator();
            //Quiz.AddQuiz();
            //QuizQuestion.AddQuizQuestion();
            //QuizQuestionAnswer.AddQuizQuestionAnswer();
            //var quiz = Quiz.GetQuiz();
            return View();
        }

        //[Route("Quiz/Question")]
        public ActionResult Question()
        {
            var viewModel = QuizQuestion.GetQuestionViewModel(2);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Results()
        {
            return View();
        }

    }
}
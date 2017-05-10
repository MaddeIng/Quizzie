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

            //QuizCreator.AddQuizCreator();
            //Quiz.AddQuiz();
            //QuizQuestion.AddQuizQuestion();
            QuizQuestionAnswer.AddQuizQuestionAnswer();


            QuizzieDBContext context = new QuizzieDBContext();
            var test = context.Quizs.Count();

            return View();
        }

    }
}
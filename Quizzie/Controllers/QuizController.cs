using Quizzie.Models;
using Quizzie.Models.Entities;
using Quizzie.Models.VM;
using System;
using System.Collections.Generic;
using System.IO;
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
        //public ActionResult Question()
        //{
        //    var viewModel = QuizQuestion.GetQuestionViewModel(2);
        //    return View(viewModel);
        //}

        // Creates a quiz
        public ActionResult NewQuiz()
        {
            var id = Quiz.NewQuiz();
            return RedirectToAction(nameof(CreateQuestion), new { quizId = id, questionId = 1 });
        }

        [HttpGet]
        public ActionResult CreateQuestion(int quizId, int questionId)
        {
            var viewModel = Quiz.GetQuestion(quizId, questionId);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateQuestion(int quizId, int questionId, string previous, string next, string done, QuizCreateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                Quiz.AddQuestion(viewModel, quizId, questionId);
            }

            return View(nameof(CreateQuestion));
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Title()
        {
            return View();
            //return RedirectToAction("Create");
        }


        public ActionResult Results()
        {
            return View();
        }
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/img"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Create", "Quiz");
        }

        public ActionResult GetPartialViewIndex()
        {
            return PartialView("_Index");
        }

        public ActionResult GetPartialViewQuestion()
        {
            return PartialView("_Question");
        }

        public ActionResult GetPartialViewResults()
        {
            return PartialView("_Results");
        }

        
    }
}
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
        public ActionResult CreateQuestion(int quizId, int questionId, string next, string done, QuizCreateVM viewModel, HttpPostedFileBase postedFile)
        {

            if (done !=null)
            {
                QuizzieDBContext context = new QuizzieDBContext();

                var accessCode = context.Quizs.SingleOrDefault(q => q.ID == quizId).AccessCode;

                ViewBag.AccessCode = accessCode;
                return RedirectToAction(nameof(Title), new { code = accessCode });
            }

            int _questionID= 0;
            
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                _questionID = Quiz.AddQuestion(viewModel, quizId, questionId);
            }

            var imageLink = GetImageLink(_questionID);
            
            FileUpload(postedFile, imageLink);

            return RedirectToAction(nameof(CreateQuestion), new { quizId = quizId, questionId = 1 });
        }

        private string GetImageLink(int questionID)
        {
            QuizzieDBContext context = new QuizzieDBContext();

            var imageLink = context.QuizQuestions.SingleOrDefault(q => q.ID == questionID).ImageLink.ToString();

            return imageLink;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Title(int code)
        {
            ViewBag.AccessCode = code;
            return View();
        }


        public ActionResult Results()
        {
            return View();
        }

        public void FileUpload(HttpPostedFileBase file, string imageLink)
        {
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                string path = Server.MapPath("~" + imageLink);
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
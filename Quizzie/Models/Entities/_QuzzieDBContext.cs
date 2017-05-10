using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Quizzie.Models.VM;

namespace Quizzie.Models.Entities
{
    public partial class QuizzieDBContext : DbContext
    {

        public QuizQuestionVM GetQuestionViewModel(int id)
        {
            QuizQuestionVM myViewModel = new QuizQuestionVM();
            QuizzieDBContext qDBcontext = new QuizzieDBContext();   //DataBasen

            var question = qDBcontext.QuizQuestions
                .FirstOrDefault(q => q.ID == id);


            var answers = qDBcontext.QuizQuestionAnswers
                .Where(a => a.QuizQuestionID == id).ToList();


            myViewModel.Question = question.Question;
            myViewModel.Answers = answers;

            return myViewModel;
        }
    }
}
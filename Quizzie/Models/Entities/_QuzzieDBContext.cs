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
            QuizQuestionVM myQQVM = new QuizQuestionVM();

            myQQVM.Question = "Hur många bultar finns det i Ölandsbron";

            //myQQVM = QuizQuestions
            //   .Where(q => q.ID == id)
            //   .Select(q => new QuizQuestionVM
            //   {
            //       Question = "Hur många bultar finns det i Ölandsbron", /*q.Question,*/

            //   });

            return myQQVM;
        }
    }
}

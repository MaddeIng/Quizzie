using Quizzie.Models.VM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Quizzie.Models.Entities
{
    public partial class QuzzieDBContext : DbContext
    {

        //public QuizQuestionVM GetQuestionViewModel(int id)
        //{
             
        //    return QuizQuestions
        //        .Where(r => r.FoodType == id)
        //        .Select(r => new RestaurantsIndexVM
        //        {
        //            Name = r.Name,
        //            WebbPage = r.WebbPage,
        //            PriceRange = r.PriceRange
        //        })
        //        .ToArray();
        //}

    }
}
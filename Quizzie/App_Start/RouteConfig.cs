using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Quizzie
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "QuizQuestions",
                url: "quiz/createquestion/{quizId}/{questionId}",
                defaults: new { controller = "Quiz", action = "CreateQuestion" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Quiz", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

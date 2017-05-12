using Microsoft.Owin;
using Owin;
using Quizzie.Models.Entities;

[assembly: OwinStartupAttribute(typeof(Quizzie.Startup))]
namespace Quizzie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            //ConfigureAuth(app);

            //QuizQuestion.AddQuizQuestion();
            //QuizQuestionAnswer.AddQuizQuestionAnswer();
        }
    }
}

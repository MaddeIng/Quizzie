using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quizzie.Startup))]
namespace Quizzie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            //ConfigureAuth(app);
        }
    }
}

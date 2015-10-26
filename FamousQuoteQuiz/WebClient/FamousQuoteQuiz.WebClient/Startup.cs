using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FamousQuoteQuiz.WebClient.Startup))]
namespace FamousQuoteQuiz.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

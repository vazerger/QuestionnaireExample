using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuestionnaireExample.Startup))]
namespace QuestionnaireExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Graders.Web.Startup))]
namespace Graders.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MCMD.Web.Startup))]
namespace MCMD.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}

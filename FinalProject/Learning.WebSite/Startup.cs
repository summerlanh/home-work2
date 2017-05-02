using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Learning.WebSite.Startup))]
namespace Learning.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

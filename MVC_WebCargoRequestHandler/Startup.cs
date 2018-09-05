using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_WebCargoRequestHandler.Startup))]
namespace MVC_WebCargoRequestHandler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(Website.App_Start.Startup))]

namespace Website.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
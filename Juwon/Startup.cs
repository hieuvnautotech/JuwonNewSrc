using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Juwon.Startup))]

namespace Juwon
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

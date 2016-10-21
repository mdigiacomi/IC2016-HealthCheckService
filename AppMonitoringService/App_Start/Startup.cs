using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(AppMonitoringService.Startup))]

namespace AppMonitoringService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                var hubConfiguration = new HubConfiguration { EnableDetailedErrors = true };
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}

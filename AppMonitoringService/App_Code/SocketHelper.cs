using Microsoft.AspNet.SignalR;

namespace AppMonitoringService.App_Code
{
    public class SocketHelper
    {
        
        public static void SendMessage(string name, string message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<StatusHub>();
            hubContext.Clients.All.SendMessage(name, message);
        }

    }
}
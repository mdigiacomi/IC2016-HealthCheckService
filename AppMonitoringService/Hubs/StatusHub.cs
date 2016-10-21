using Microsoft.AspNet.SignalR;

namespace AppMonitoringService.App_Code
{
    public class StatusHub : Hub
    {
        public void SendMessage(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }
    }
}
using AppMonitoringService.App_Code;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppMonitoringService.Controllers.API
{
    public class PingController : ApiController
    {
        // GET: api/Ping
        public IEnumerable<string> Get()
        {
            SocketHelper.SendMessage("Ping", "Pong");

            return new string[] { "Ping", "Pong" };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppMonitoringService.Models
{
    public class HealthCheckModel
    {
        public string AppName;
        public string AppUrl;
        public string Status;
        public string Message;
        public DateTime TimeStamp;
        List<Dependency> Dependencies;

        public HealthCheckModel()
        {
            Dependencies = new List<Dependency>();
        }
    }

    public class Dependency
    {
        public string DepName;
        public string DepStatus;
        public string DepStatusMessage;
    }
}
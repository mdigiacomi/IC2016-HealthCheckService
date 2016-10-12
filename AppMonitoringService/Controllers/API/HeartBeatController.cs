using AppMonitoringService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElasticSearchFacade.Controllers.API
{
    public class HeartBeatController : ApiController
    {
        // GET: api/HeartBeat
        public HealthCheckModel Get()
        {
            HealthCheckModel hcm = new HealthCheckModel();

            hcm.AppName = "ElasticSearchFacade";
            hcm.AppUrl = "http://dev.michaeldigiacomi.com/IC2016/ESAppHealth/Help";
            hcm.Status = "200";
            hcm.Message = "Application Online";
            hcm.TimeStamp = DateTime.Now;

            return hcm;
        }
    }
}

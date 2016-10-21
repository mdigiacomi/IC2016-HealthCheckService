using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RestSharp;

using AppMonitoringService.Models;
using AppMonitoringService.App_Code;
using System.Configuration;
using Newtonsoft.Json;
using ElasticSearchFacade.Models;
using Microsoft.AspNet.SignalR;
using ElasticSearchFacadeSevice.App_Code;

namespace AppMonitoringService.Controllers.API
{
    public class HealthCheckController : ApiController
    {

        string ESAPHealthURL = ConfigurationManager.AppSettings["ESAppHealthURL"];
        string AppInfo = ConfigurationManager.AppSettings["GetAppInfoEndpoint"];
        string UpdateAppInfo = ConfigurationManager.AppSettings["UpdateAppInfoEndpoint"];
        IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<StatusHub>();

        // GET: api/HealthCheck
        public string Get()
        {
            try
            {

                var client = new RestClient(ESAPHealthURL + AppInfo);
                var request = new RestRequest("", Method.GET);
                var queryResult = client.Execute(request);

                List<appinfo> Apps = JsonConvert.DeserializeObject<List<appinfo>>(queryResult.Content);

                foreach (appinfo app in Apps)
                {

                    filewriter.writelog(app.AppName +  ": Checking Service");

                    DeleteObject deleteme = new DeleteObject();

                    deleteme.AppName = app.AppName;
                    deleteme.AppUrl = app.AppURL;

                    HealthCheckModel appstatus = Post(deleteme);

                }

            }
            catch (Exception error)
            {
                return error.Message;
            }

            return "DONE";
        }

        // POST: api/HealthCheck
        public HealthCheckModel Post([FromBody] DeleteObject deleteme)
        {
            string res = "";

            HealthCheckModel hcm = new HealthCheckModel();
            List<HealthCheckModel> returnval = new List<HealthCheckModel>();

            var client = new RestClient(deleteme.AppUrl);
            var request = new RestRequest("", Method.GET);
            var queryResult = client.Execute(request);

            hcm.AppName = deleteme.AppName;
            hcm.AppUrl = deleteme.AppUrl;
            hcm.TimeStamp = DateTime.Now;

            hcm.Status = queryResult.StatusCode.ToString();

            if(queryResult.StatusCode == 0)
            {
                hcm.Status = HttpStatusCode.ServiceUnavailable.ToString();
            }

            if (JSONHelper.IsValidJson(queryResult.Content)) 
                hcm.Message = queryResult.Content;

            ElasticSearch.LogHealthMetric(hcm);

            return hcm;

        }
    }
}

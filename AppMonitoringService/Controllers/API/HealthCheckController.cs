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

namespace AppMonitoringService.Controllers.API
{
    public class HealthCheckController : ApiController
    {

        string ESAPHealthURL = ConfigurationManager.AppSettings["ESAppHealthURL"];
        string AppInfo = ConfigurationManager.AppSettings["GetAppInfoEndpoint"];
        string UpdateAppInfo = ConfigurationManager.AppSettings["UpdateAppInfoEndpoint"];

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
                    Dictionary<string, string> HealthCheckURL = new Dictionary<string, string>();

                    HealthCheckURL.Add(app.AppName, app.AppURL);

                    List<HealthCheckModel> appstatus = Post(HealthCheckURL);

                    client = new RestClient(ESAPHealthURL + UpdateAppInfo);
                    request = new RestRequest("", Method.PUT);
                    request.AddBody(appstatus[0]);
                    queryResult = client.Execute(request);

                }

            }
            catch (Exception error)
            {
                return error.Message;
            }

            return "DONE";
        }

        // POST: api/HealthCheck
        public List<HealthCheckModel> Post([FromBody]Dictionary<string, string> HealthCheckURLs)
        {
            string res = "";

            HealthCheckModel hcm = new HealthCheckModel();
            List<HealthCheckModel> returnval = new List<HealthCheckModel>();

            foreach (KeyValuePair<string,string> url in HealthCheckURLs)
            {
                var client = new RestClient(url.Value);
                var request = new RestRequest("", Method.GET);
                var queryResult = client.Execute(request);

                hcm.AppName = url.Key;
                hcm.AppUrl = url.Value;
                hcm.TimeStamp = DateTime.Now;
                hcm.Status = queryResult.StatusCode.ToString();
                if (JSONHelper.IsValidJson(queryResult.Content)) ;
                hcm.Message = queryResult.Content;

                ElasticSearch.LogHealthMetric(hcm);

                returnval.Add(hcm);
            }

            return returnval;

        }
    }
}

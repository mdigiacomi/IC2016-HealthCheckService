using AppMonitoringService.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AppMonitoringService.App_Code
{
    public class ElasticSearch
    {
        public static IEnumerable<BulkResponseItemBase> LogHealthMetric(HealthCheckModel hcm)
        {
            var node = new Uri(ConfigurationManager.AppSettings["ElasticSearchURL"]);
            var settings = new ConnectionSettings(node).RequestTimeout(TimeSpan.FromSeconds(60));
            var client = new ElasticClient(settings);

            List<HealthCheckModel> HeartBeats = new List<HealthCheckModel>();

            HeartBeats.Add(hcm);

            var result = client.IndexMany<HealthCheckModel>(HeartBeats, "healthcheck", null);

            return result.ItemsWithErrors;
        }
    }
}
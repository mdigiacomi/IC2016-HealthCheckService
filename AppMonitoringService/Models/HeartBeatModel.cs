using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElasticSearchFacade.Models
{
    [Serializable]
    public class appinfo
    {
        public string ID;
        public string AppName;
        public string AppDesc;
        public string AppURL;
        public string AppVersion;
        public Dictionary<string, string> AdditionalParameters;
        public string AppStatus;

        public appinfo()
        {
            AdditionalParameters = new Dictionary<string, string>();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace WebScraperAPI.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root+"/"+Version;

        public static class Services
        {
            public const string GetServices = Base+"/services";

        }
        public static class IPScraper
        {
            public const string GetInformation = Base + "/ipscraper";

        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Integrations.DomainAvailability;
using Integrations.GeoIP;
using Integrations.Ping;
using Integrations.Port;
using Integrations.RDAP;
using Integrations.ReverseDNS;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Integrations.Common
{
    public class Process
    {

        public async Task<JObject> startProcess(string IP, int Service)
        {
            if (Service < 0 || Service > 5)
            {
                JArray array = new JArray();
                array.Add("Service Not Found");

                JObject o = new JObject();
                o["Error"] = array;

                return o;

            }
            if (IP == null || IP == "")
            {
                JArray array = new JArray();
                array.Add("Invalid IP or Domain");

                JObject o = new JObject();
                o["Error"] = array;

                return o;
            }


            JObject ReturnInfo = null;
            string NewIP = IP;
            if(Service != 3 && Service != 4)
            {
                RequestReverseDNS DNS = new RequestReverseDNS();
                NewIP = await DNS.GetRealIP(IP);
            }
           


            switch (Service)
            {
                //Call all Services
                case 0:
                    JObject Geo = await RequestGeo(NewIP);
                    JObject Ping = await RequestPing(NewIP);
                    JObject ReverseDNS = await GetReverseDNS(IP);
                    JObject DomainAv = await RequestDomainAv(IP);
                    JObject OpenPort = await RequestPort(NewIP);
                    Geo.Merge(Ping, new JsonMergeSettings
                    {
                        // union array values together to avoid duplicates
                        MergeArrayHandling = MergeArrayHandling.Union
                    });
                    Geo.Merge(ReverseDNS, new JsonMergeSettings
                    {
                        // union array values together to avoid duplicates
                        MergeArrayHandling = MergeArrayHandling.Union
                    });
                    Geo.Merge(DomainAv, new JsonMergeSettings
                    {
                        // union array values together to avoid duplicates
                        MergeArrayHandling = MergeArrayHandling.Union
                    });
                    Geo.Merge(OpenPort, new JsonMergeSettings
                    {
                        // union array values together to avoid duplicates
                        MergeArrayHandling = MergeArrayHandling.Union
                    });
                    ReturnInfo = Geo;

                    break;
                //Call geoAPI
                case 1:
                    ReturnInfo = await RequestGeo(NewIP);
                    break;
                //Call Ping
                case 2:
                    ReturnInfo = await RequestPing(NewIP);
                    break;
                //Call ReverseDNS
                case 3:
                    ReturnInfo = await GetReverseDNS(NewIP);
                    break;
                //Call Domain Availabily
                case 4:
                    ReturnInfo = await RequestDomainAv(NewIP);
                    break;
                //Call Port
                case 5:
                    ReturnInfo = await RequestPort(NewIP);
                    break;

            }
        
            return ReturnInfo;

        }

        public async Task<JObject> RequestGeo(string IP)
        {
            RequestGeo G = new RequestGeo();
            string sIP = "http://ip-api.com/json/"+IP;
            return await G.GetRequestGeo(sIP); 
        }

        public async Task<JObject> RequestPing(string IP)
        {
            RequestPing P = new RequestPing();
            return await P.getRequestPing(IP);
        }

        public async Task<JObject> RequestRDAP(string IP)
        {
            RequestRDAP RDAP = new RequestRDAP();
            string sIP = "https://www.rdap.net/ip/" + IP.Trim();
            return await RDAP.GetRequestRDAP(sIP);
        }

        public async Task<JObject> GetReverseDNS(string IP)
        {
            RequestReverseDNS RDNS = new RequestReverseDNS();

            return await RDNS.GetReverseDNS(IP);
        }

        public async Task<JObject> RequestPort(string IP)
        {
            RequestPort Port = new RequestPort();

            return await Port.getRequestPort(IP);
        }

        public async Task<JObject> RequestDomainAv(string IP)
        {
            DomainAv DAV = new DomainAv();
            string sIP = "https://domain-availability.whoisxmlapi.com/api/v1?apiKey=at_dioJkDRhKOjfDALCU55u2lMInw4uL&domainName="+ IP + "&credits=DA";
            return await DAV.GetDomainAv(sIP);
        }


    }
}

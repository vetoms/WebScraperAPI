using Integrations.Ping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.ReverseDNS
{
    public class RequestReverseDNS
    {

        string sAPIKEY = "fe57a4a2586aa4a5a95aea3a0b3b6a013e6e5bc3";
        public bool CheckValidIP(string IP)
        {
            bool isOK = false;
            IPAddress address;
            if (IP.Contains(".") || IP.Contains(":"))
            {
                if (IPAddress.TryParse(IP, out address))
                {
                    switch (address.AddressFamily)
                    {
                        case System.Net.Sockets.AddressFamily.InterNetwork:
                            isOK = true;
                            break;
                        case System.Net.Sockets.AddressFamily.InterNetworkV6:
                            isOK = true;
                            break;
                    }
                }

            }
               
            return isOK;

        }

        public async Task<string> GetRealIP(string IPorDomain)
        {
            string sResult = IPorDomain;
            if (!CheckValidIP(IPorDomain))
            {
                RequestPing P = new RequestPing();
                ReturnPing RV = await P.getRequestPingObj(IPorDomain);
                sResult = RV.Address;
            }
            return sResult;
        }

        public async Task<string> GetRealDomain(string IPorDomain)
        {
            string sResult = IPorDomain;
            if (CheckValidIP(IPorDomain))
            {
                ReturnReverseDNS Host = new ReturnReverseDNS();
                string sIP = "https://api.viewdns.info/reversedns/?ip=" + IPorDomain + "&apikey=" + sAPIKEY + "&output=json";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    //For IP-API
                    client.BaseAddress = new Uri(sIP);
                    //HttpResponseMessage response = client.GetAsync(API_PATH_IP_API).GetAwaiter().GetResult();
                    HttpResponseMessage response = await client.GetAsync(sIP);
                    if (response.IsSuccessStatusCode)
                    {
                        Host = response.Content.ReadAsAsync<ReturnReverseDNS>().GetAwaiter().GetResult();
                    }
                }
                int Count = 0;
                if(Host.response.domains != null)
                {
                    foreach (var Domain in Host.response.domains)
                    {
                        if (Count == 0)
                        {
                            sResult = Domain;
                        }
                    }
                }
            }
            else
            {
                sResult = IPorDomain;
            }
            return sResult;
        }



        public async Task<JObject> GetReverseDNS(string IPorDomain)
        {
            JObject R = new JObject();


            if (CheckValidIP(IPorDomain))
            {
                JObject Information = await getHost(IPorDomain);
                if(Information != null)
                {
                    R = Information;
                }
               

            }
            else
            {
                JObject Information = await getIP(IPorDomain);
                if (Information != null)
                {
                    R = Information;
                }
            }

            return R;
        }


        public async Task<JObject> getHost(string IP)
        {
            try
            {

                string sIP = "https://api.viewdns.info/reversedns/?ip=" + IP + "&apikey=" + sAPIKEY + "&output=json";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    //For IP-API
                    client.BaseAddress = new Uri(sIP);
                    //HttpResponseMessage response = client.GetAsync(API_PATH_IP_API).GetAwaiter().GetResult();
                    HttpResponseMessage response = await client.GetAsync(sIP);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<JObject>(jsonString);
                    }

                }
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }

        public async Task<JObject> getIP(string IP)
        {
            try
            {

                string sIP = " https://api.viewdns.info/reversedns/?host=" + IP + "&apikey=" + sAPIKEY + "&output=json";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    //For IP-API
                    client.BaseAddress = new Uri(sIP);
                    //HttpResponseMessage response = client.GetAsync(API_PATH_IP_API).GetAwaiter().GetResult();
                    HttpResponseMessage response = await client.GetAsync(sIP);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<JObject>(jsonString);
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }




    }
}

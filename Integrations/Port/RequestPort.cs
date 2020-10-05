using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Port
{
    public class RequestPort
    {

        string sAPIKEY = "fe57a4a2586aa4a5a95aea3a0b3b6a013e6e5bc3";


        public async Task<JObject> getRequestPort(string IP)
        {
            try
            {

                string sIP = " https://api.viewdns.info/portscan/?host=" + IP + "&apikey=" + sAPIKEY + "&output=json";
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

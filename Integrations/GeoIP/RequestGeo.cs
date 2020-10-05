using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.GeoIP
{
    public class RequestGeo
    {
        
        public async Task<JObject> GetRequestGeo(string IP)
        {
            ReturnGeo locationDetails = new ReturnGeo();
                
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //For IP-API
                client.BaseAddress = new Uri(IP);
                //HttpResponseMessage response = client.GetAsync(API_PATH_IP_API).GetAwaiter().GetResult();
                HttpResponseMessage response = await client.GetAsync(IP);
                if (response.IsSuccessStatusCode)
                {
                    locationDetails = response.Content.ReadAsAsync<ReturnGeo>().GetAwaiter().GetResult();                   
                }
            }

            JObject J = JObject.FromObject(locationDetails);
            return J;

        }




    }
}

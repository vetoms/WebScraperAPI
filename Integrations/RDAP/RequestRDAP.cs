using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.RDAP
{
    public class RequestRDAP
    {
        public async Task<JObject> GetRequestRDAP(string IP)
        {
            try
            {

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                
                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    //For IP-API
                    client.BaseAddress = new Uri(IP);
                    //HttpResponseMessage response = client.GetAsync(API_PATH_IP_API).GetAwaiter().GetResult();
                    HttpResponseMessage response = await client.GetAsync(IP);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<JObject>(jsonString);
                    }
                }

            }
            catch(Exception ex)
            {

            }

           
            return null;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Integrations.Ping
{
    public class RequestPing
    {

        public async Task<JObject> getRequestPing(string nameOrAddress)
        {
            ReturnPing R = new ReturnPing();

            try
            {
                System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
                PingReply reply = await pingSender.SendPingAsync(nameOrAddress);


                R.PingStatus = reply.Status.ToString();


                if (reply.Address != null)
                {
                    R.Address = reply.Address.ToString();
                }
                else
                {
                    R.Address = "Error";
                }
            }
            catch (Exception ex)
            {
                R.PingStatus = "Error: "+ex.Message;
                R.Address = "Error";
            }

            JObject J = JObject.FromObject(R);
            return J;
        }

        public async Task<ReturnPing> getRequestPingObj(string nameOrAddress)
        {
            ReturnPing R = new ReturnPing();

            try
            {
                System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
                PingReply reply = await pingSender.SendPingAsync(nameOrAddress);


                R.PingStatus = reply.Status.ToString();


                if (reply.Address != null)
                {
                    R.Address = reply.Address.ToString();
                }
                else
                {
                    R.Address = "Error";
                }
            }
            catch (Exception ex)
            {
                R.PingStatus = "Error: " + ex.Message;
                R.Address = "Error";
            }

           
            return R;
        }

    }
}

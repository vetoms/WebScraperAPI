using System;
using System.Collections.Generic;
using System.Text;

namespace Integrations.ReverseDNS
{
    public class ReturnReverseDNS
    {
        public query query { get; set; }
        public response response { get; set; }
    }



    public class query
    {
        public string tool { get; set; }
        public string host { get; set; }
        public string ip { get; set; }
    }

    public class response
    {
        public string rdns { get; set; }
        public string domain_count { get; set; }
        public List<string> domains { get; set; }
    }
}

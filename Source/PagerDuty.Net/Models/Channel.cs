using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class Channel {
        //Default fields
        public string type { get; set; }
        public string summary { get; set; }

        //Channel specific fields

        //Nagios, API, Web trigger channel specific
        public object details { get; set; }

        //Nagios 
        public string host { get; set; }
        public string service { get; set; }
        public string state { get; }

        //API channel specific
        public string service_key { get; set; }
        public string description { get; set;  }
        public string incident_key { get; set; }

        //Web trigger, Email channel specific
        public string subject { get; set; }

        //Email channel specific
        public string to { get; set; }
        public string from { get; set; }
        public string body { get; set; }
        public string body_content_type { get; set; }
        public string raw_url { get; set; }
        public string html_url { get; set; }

        //Website channel specific
        public int duration { get; set; }
    }
}

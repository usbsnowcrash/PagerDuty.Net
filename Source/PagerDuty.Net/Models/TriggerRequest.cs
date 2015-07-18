using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class TriggerRequest {
        public string service_key { get; set; }
        public string event_type { get; set; }
        public string description { get; set; }
        public string incident_key { get; set; }
        public string client { get; set; }
        public string client_url { get; set; }
        public object details { get; set; }
        public Context context { get; set; }
    }
}

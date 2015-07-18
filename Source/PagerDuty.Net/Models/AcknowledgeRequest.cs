using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class AcknowledgeRequest {
        public string service_key { get; set; }
        public string event_type { get; set; }
        public string incident_key { get; set; }
        public string description { get; set; }
        public object details { get; set; }
    }
}

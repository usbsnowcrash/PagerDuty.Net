using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class IntegrationResponse {
        public string status { get; set; }
        public string message { get; set; }
        public string incident_key { get; set; }
    }
}

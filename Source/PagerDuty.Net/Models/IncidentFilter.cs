using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class IncidentFilter {
        public bool ReturnAll { get; set; }
        public DateTime since { get; set; }
        public DateTime until { get; set; }
        public string fields { get; set; }
        public string status { get; set; }
        public string incident_key { get; set; }
        public string service { get; set; }
        public string[] user_ids { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class Schedule {
        public string id { get; set; }
        public string name { get; set; }
        public string time_zone { get; set; }
        public DateTime today { get; set; }
        public string description { get; set; }
        public List<EscalationPolicy> escalation_policies { get; set; }
    }
}

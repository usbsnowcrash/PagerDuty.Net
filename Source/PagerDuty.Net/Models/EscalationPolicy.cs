using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class EscalationPolicy {
        public string id { get; set; }
        public string name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class AlertsResponse {
        public List<Alert> alerts { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int total { get; set; }
    }
}

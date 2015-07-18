using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable]
    public class ScheduleEntriesFilter {
        public DateTime since { get; set; }
        public DateTime until { get; set; }
        public bool overflow { get; set; }
        public string time_zone { get; set; }
        public string user_id { get; set; }
    }
}

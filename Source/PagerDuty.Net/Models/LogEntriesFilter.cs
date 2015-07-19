using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class LogEntriesFilter {
        public DateTime since { get; set; }
        public DateTime until { get; set; }
        public string time_zone { get; set; }
        public bool is_overview { get; set; }
        public List<string> include { get; set; }
    }
}

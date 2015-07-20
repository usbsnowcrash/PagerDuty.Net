using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class ScheduleEntry {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public User user { get; set; }
    }
}

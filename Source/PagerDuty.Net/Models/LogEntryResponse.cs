using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class LogEntryResponse {
        public LogEntry log_entry { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class Incident {
        public string id { get; set; }
        public string summary { get; set; }
        public string incident_number { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public string html_url { get; set; }
        public string incident_key { get; set; }
        public Service service { get; set; }
        public User last_status_change_by { get; set; }
        public DateTime last_status_change_at { get; set; }
        public LogEntry first_trigger_log_entry { get; set; }
    }
}

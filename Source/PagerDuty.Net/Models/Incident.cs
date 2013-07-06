using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class Incident {
        public string id { get; set; }
        public string incident_number { get; set; }
        public string status { get; set; }
        public DateTime created_on { get; set; }
        public string html_url { get; set; }
        public string incident_key { get; set; }
        public Service service { get; set; }
        public User assigned_to_user { get; set; }
        public User last_status_change_by { get; set; }
        public DateTime last_status_change_on { get; set; }
        public string trigger_summary_data { get; set; }
        public string trigger_details_html_url { get; set; }
        public int number_of_escalations { get; set; }
    }
}

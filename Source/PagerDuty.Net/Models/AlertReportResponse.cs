using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class AlertReportResponse {
        public List<AlertReport> alerts { get; set; }
        public int total_number_of_alerts { get; set; }
        public int total_number_of_phone_alerts { get; set; }
        public int total_number_of_sms_alerts { get; set; }
        public int total_number_of_email_alerts { get; set; }
        public int total_number_of_billable_alerts { get; set; }
    }
}

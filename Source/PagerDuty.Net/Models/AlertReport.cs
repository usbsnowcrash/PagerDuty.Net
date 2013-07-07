using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class AlertReport {
        public int number_of_alerts { get; set; }
        public int number_of_phone_alerts { get; set; }
        public int number_of_sms_alerts { get; set; }
        public int number_of_email_alerts { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}

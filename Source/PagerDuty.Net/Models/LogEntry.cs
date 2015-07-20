using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class LogEntry {
        //Default fields
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public string type { get; set; }
        public Agent agent { get; set; }
        public Channel channel { get; set; }
        public string note { get; set; }
        public Context context { get; set; }

        //Assign, escalate log specific fields
        public User assigned_user { get; set; }

        //Notify log specific fields
        public User user { get; set; }
        public Notification notification { get; set; }

        //Acknowledge log specific fields
        public int acknowledgement_timeout { get; set; }
    }
}

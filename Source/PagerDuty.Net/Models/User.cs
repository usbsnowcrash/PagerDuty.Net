using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class User {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string time_zone { get; set; }
        public string color { get; set; }
        public string role { get; set; }
        public string avatar_url { get; set; }
        public string user_url { get; set; }
        public bool invitation_sent { get; set; }
        public bool marketing_opt_out { get; set; }

    }
}

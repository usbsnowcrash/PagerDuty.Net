using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class Alert {
        public string id { get; set; }
        public string type { get; set; }
        public DateTime started_at { get; set; }
        public User user { get; set; }
        public string address { get; set; }
    }
}

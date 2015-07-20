using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    public class Notification {
        public string type { get; set; }
        public string status { get; set; }
        public string address { get; set; }
    }
}

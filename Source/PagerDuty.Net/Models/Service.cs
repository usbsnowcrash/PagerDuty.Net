using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class Service {
        public string id { get; set; }
        public string name { get; set; }
        public string html_url { get; set; }
    }
}

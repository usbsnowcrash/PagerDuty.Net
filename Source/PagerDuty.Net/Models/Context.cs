using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class Context {
        public string type { get; set; }
        public string href { get; set; }
        public string text { get; set; }
        public string src { get; set; }
        public string alt { get; set; }
    }
}

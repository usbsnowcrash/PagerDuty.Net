using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net {
    [Serializable()]
    public class Agent: User {
        public string type { get; set; }
    }
}

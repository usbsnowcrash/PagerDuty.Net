using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class Note {
        public string id { get; set; }
        public string content { get; set; }
        public DateTime created_at { get; set; }
        public User user { get; set; }
    }
}

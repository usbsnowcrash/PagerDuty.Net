using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net
{
    [Serializable()]
    public class SchedulesResponse
    {
        public List<Schedule> schedules { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int total { get; set; }
    }
}

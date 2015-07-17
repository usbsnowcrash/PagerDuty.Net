using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net
{
    [Serializable]
    public class ScheduleEntriesResponse
    {
        public int total { get; set; }
        public List<ScheduleEntry> entries { get; set; }
    }
}

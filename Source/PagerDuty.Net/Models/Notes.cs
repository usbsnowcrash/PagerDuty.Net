using System;
using System.Collections.Generic;

namespace PagerDuty.Net
{
  [Serializable()]
  public class Notes
  {
    public List<Note> notes { get; set; }
  }
}
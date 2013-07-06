using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    public class PagerDutyAPIException : Exception {
        IRestResponse Response { get; set; }

        public PagerDutyAPIException(IRestResponse response) : base() {
            Response = response;
        }

    }
}

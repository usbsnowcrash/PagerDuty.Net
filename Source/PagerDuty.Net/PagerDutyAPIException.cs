using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class PagerDutyAPIException : Exception {
        IRestResponse Response { get; set; }

        public PagerDutyAPIException(IRestResponse response)
            : base() {
            Response = response;
        }

        protected PagerDutyAPIException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            Response = (IRestResponse)info.GetValue("Response", typeof(IRestResponse));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
            info.AddValue("Response", Response);
        }

    }
}

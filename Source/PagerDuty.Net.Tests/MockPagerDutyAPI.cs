using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net.Tests {
    public class MockPagerDutyAPI : PagerDutyAPI {
        public IRestClient Client { get; set; }
        public IRestRequest Request { get; set; }

        public MockPagerDutyAPI(IRestClient client, IRestRequest request, string domain, string token)
            : base(domain, token) {
                Client = client;
                Request = request;
        }

        protected override IRestClient GetClient(string url) {
            return Client;
        }

        protected override IRestRequest GetRequest() {
            return Request;
        }
    }
}

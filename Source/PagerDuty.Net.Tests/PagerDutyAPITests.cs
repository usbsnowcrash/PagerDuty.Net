using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PagerDuty.Net;
using RestSharp;
using Moq;

namespace PagerDuty.Net.Tests {
    [TestClass]
    public class PagerDutyAPITests {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CTOR_PassedBadDomain_ThrowsException() {
            var api = new PagerDutyAPI(null,"Value");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CTOR_PassedBadToken_ThrowsException() {
            var api = new PagerDutyAPI("Value","");
        }

        [TestMethod]
        public void CTOR_SetsCorrectValues() {
            var api = new PagerDutyAPI("domain","token");
            Assert.AreEqual("domain",api.Subdomain);
            Assert.AreEqual("token",api.AccessToken);
        }

        [TestMethod]
        public void GetAlerts_PerformsCorrectRequest() {
            //Setup
            var response = new RestResponse<AlertsResponse> { Data = new AlertsResponse() };
            var since = DateTime.Now.AddDays(-10);
            var until = DateTime.Now.AddDays(-10);

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("since", since.ToString("s")));
            restReq.Setup(x => x.AddParameter("until", until.ToString("s")));
            restReq.Setup(x => x.AddParameter("offset", 0));
            restReq.Setup(x => x.AddParameter("limit", 100));
            
            var restClient = new Mock<RestClient>();
            restClient.Setup(x => x.Execute<AlertsResponse>(It.IsAny<IRestRequest>())).Returns(response);

            var api = new MockPagerDutyAPI(restClient.Object,restReq.Object, "domain", "token");
            api.GetAlerts(since, until, Filter.Unspecified);

            //Assert
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PagerDuty.Net;
using RestSharp;
using Moq;
using System.Collections.Generic;

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

        [TestMethod]
        public void GetIncident_PerformsCorrectRequest() {
            //Setup
            var response = new RestResponse<Incident> { Data = new Incident() };
            var restReq = new Mock<IRestRequest>();
            var restClient = new Mock<RestClient>();
            restClient.Setup(x => x.Execute<Incident>(It.IsAny<IRestRequest>())).Returns(response);

            var api = new MockPagerDutyAPI(restClient.Object, restReq.Object, "domain", "token");
            api.GetIncident("PIJ90N7");

            //Assert
            restReq.VerifyAll();
            restClient.VerifyAll();
        }

        [TestMethod]
        public void GetIncidents_PerformsCorrectRequest() {
            //Setup
            var response = new RestResponse<IncidentsResponse> { Data = new IncidentsResponse() };
            var since = DateTime.Now.AddDays(-10);
            var until = DateTime.Now;

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("since", since.ToString("s")));
            restReq.Setup(x => x.AddParameter("assigned_to_user", "bob,jeff"));
            restReq.Setup(x => x.AddParameter("until", until.ToString("s")));
            restReq.Setup(x => x.AddParameter("sort_by", "incident_number:desc"));
            restReq.Setup(x => x.AddParameter("offset", 3));
            restReq.Setup(x => x.AddParameter("limit", 1000));

            var restClient = new Mock<RestClient>();
            restClient.Setup(x => x.Execute<IncidentsResponse>(It.IsAny<IRestRequest>())).Returns(response);

            var api = new MockPagerDutyAPI(restClient.Object, restReq.Object, "domain", "token");
            api.GetIncidents(new IncidentFilter() { since = since, until = until, assigned_to_user = "bob,jeff" }, IncidentSortBy.incident_number, SortDirection.desc, 3, 1000);

            //Assert
            restReq.VerifyAll();
            restClient.VerifyAll();
        }

        [TestMethod]
        public void GetIncidentNotes_PerformsCorrectRequest() {
            //Setup
            var response = new RestResponse<List<Note>> { Data = new List<Note>() };
            var restReq = new Mock<IRestRequest>();

            var restClient = new Mock<RestClient>();
            restClient.Setup(x => x.Execute<List<Note>>(It.IsAny<IRestRequest>())).Returns(response);

            var api = new MockPagerDutyAPI(restClient.Object, restReq.Object, "domain", "token");
            api.GetNotesForIncident("EXAMPLE");

            //Assert
            restReq.VerifyAll();
            restClient.VerifyAll();
        }

        [TestMethod]
        public void PostNoteForIncident_PerformsCorrectRequest() {
            //Setup
            var response = new RestResponse<Note> { Data = new Note() };
            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("application/json; charset=utf-8", "{\"requester_id\":\"reqid\",\"note\":{\"content\":\"note\"}}", ParameterType.RequestBody));
            restReq.SetupSet(x => x.Method = Method.POST);
            restReq.SetupSet(x => x.RequestFormat = DataFormat.Json);

            var restClient = new Mock<RestClient>();
            restClient.Setup(x => x.Execute<Note>(It.IsAny<IRestRequest>())).Returns(response);

            var api = new MockPagerDutyAPI(restClient.Object, restReq.Object, "domain", "token");
            api.PostNoteForIncident("note", "id", "reqid");

            //Assert
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}

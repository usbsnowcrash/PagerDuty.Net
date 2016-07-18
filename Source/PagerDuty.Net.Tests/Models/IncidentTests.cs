using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;
using RestSharp;

namespace PagerDuty.Net.Tests {
    [TestClass]
    public class IncidentTests {

        [TestMethod]
        [DeploymentItem("Models\\Incident.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "Incident.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();
            
            response.Content = json;

            var myAlert = ds.Deserialize<Incident>(response);

            Assert.IsNotNull(myAlert);
            Assert.IsNotNull(myAlert.service);
            Assert.IsNotNull(myAlert.last_status_change_by);
            
            Assert.AreEqual("1", myAlert.incident_number);
            Assert.AreEqual(new DateTime(634830005610000000), myAlert.created_at);
            Assert.AreEqual("resolved", myAlert.status);
            Assert.AreEqual("https://acme.pagerduty.com/incidents/P2A6J96", myAlert.html_url);
            Assert.AreEqual(null, myAlert.incident_key);
            Assert.AreEqual(new DateTime(634830006590000000), myAlert.last_status_change_at);
        }
    }
}

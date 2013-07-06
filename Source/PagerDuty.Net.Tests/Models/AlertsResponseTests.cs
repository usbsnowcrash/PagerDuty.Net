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
    public class AlertsResponseTests {

        [TestMethod]
        [DeploymentItem("Models\\Alerts.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "Alerts.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();

            response.Content = json;

            var myAlerts = ds.Deserialize<AlertsResponse>(response);

            Assert.IsNotNull(myAlerts);
            Assert.IsNotNull(myAlerts.alerts);

            Assert.AreEqual(2, myAlerts.alerts.Count);
            Assert.AreEqual(100, myAlerts.limit);
            Assert.AreEqual(0, myAlerts.offset);
            Assert.AreEqual(2, myAlerts.total);
        }
    }
}

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
    public class IncidentsResponseTests {

        [TestMethod]
        [DeploymentItem("Models\\IncidentsResponse.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "IncidentsResponse.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();
            
            response.Content = json;

            var myAlert = ds.Deserialize<IncidentsResponse>(response);

            Assert.IsNotNull(myAlert);
            Assert.IsNotNull(myAlert.incidents);

            Assert.AreEqual(2, myAlert.incidents.Count);
            Assert.AreEqual(100, myAlert.limit);
            Assert.AreEqual(0, myAlert.offset);
            Assert.AreEqual(2, myAlert.total);
        }
    }
}

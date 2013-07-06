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
    public class ServiceTests {

        [TestMethod]
        [DeploymentItem("Models\\Service.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "Service.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();
            
            response.Content = json;

            var mySVC = ds.Deserialize<Service>(response);

            Assert.IsNotNull(mySVC);
            Assert.AreEqual("PBF77WY", mySVC.id);
            Assert.AreEqual("Generic Api", mySVC.name);
            Assert.AreEqual("https://acme.pagerduty.com/services/PBF77WY", mySVC.html_url);
        }
    }
}

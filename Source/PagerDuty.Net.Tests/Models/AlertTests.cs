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
    public class AlertTests {

        [TestMethod]
        [DeploymentItem("Models\\Alert.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "Alert.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();
            
            response.Content = json;

            var myAlert = ds.Deserialize<Alert>(response);

            Assert.IsNotNull(myAlert);
            Assert.IsNotNull(myAlert.user);
            Assert.AreEqual("PWL7QXS", myAlert.id);
            Assert.AreEqual("Phone", myAlert.type);
            Assert.AreEqual("+15555551234", myAlert.address);
            Assert.AreEqual(new DateTime(634981805300000000), myAlert.started_at);
        }
    }
}

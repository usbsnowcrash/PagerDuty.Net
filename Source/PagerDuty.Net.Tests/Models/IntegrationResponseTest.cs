using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using RestSharp.Deserializers;
using RestSharp;

namespace PagerDuty.Net.Tests.Models
{
    [TestClass]
    public class IntegrationResponseTest
    {
        [TestMethod]
        [DeploymentItem("Models\\IntegrationResponse.json")]
        public void JSONDeserializationTest()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "IntegrationResponse.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();

            response.Content = json;

            var myResponse = ds.Deserialize<IntegrationResponse>(response);

            Assert.IsNotNull(myResponse);

            Assert.AreEqual("success", myResponse.status);
            Assert.AreEqual("Event processed", myResponse.message);
            Assert.AreEqual("srv01/HTTP", myResponse.incident_key);
        }
    }
}

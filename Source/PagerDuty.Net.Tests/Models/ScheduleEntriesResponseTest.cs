using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using RestSharp.Deserializers;
using RestSharp;

namespace PagerDuty.Net.Tests.Models
{
    [TestClass]
    public class ScheduleEntriesResponseTest
    {
        [TestMethod]
        [DeploymentItem("Models\\ScheduleEntries.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "ScheduleEntries.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();

            response.Content = json;

            var myScheduleEntries = ds.Deserialize<ScheduleEntriesResponse>(response);

            Assert.IsNotNull(myScheduleEntries);
            Assert.IsNotNull(myScheduleEntries.entries);

            Assert.AreEqual(2, myScheduleEntries.total);
        }
    }
}

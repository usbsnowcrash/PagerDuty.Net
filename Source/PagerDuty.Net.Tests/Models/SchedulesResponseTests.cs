using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using RestSharp.Deserializers;
using RestSharp;

namespace PagerDuty.Net.Tests.Models
{
    [TestClass]
    public class SchedulesResponseTests
    {
        [TestMethod]
        [DeploymentItem("Models\\Schedules.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "Schedules.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();

            response.Content = json;

            var mySchedules = ds.Deserialize<SchedulesResponse>(response);

            Assert.IsNotNull(mySchedules);
            Assert.IsNotNull(mySchedules.schedules);

            Assert.AreEqual(2, mySchedules.schedules.Count);
            Assert.AreEqual(100, mySchedules.limit);
            Assert.AreEqual(0, mySchedules.offset);
            Assert.AreEqual(2, mySchedules.total);
        }
    }
}

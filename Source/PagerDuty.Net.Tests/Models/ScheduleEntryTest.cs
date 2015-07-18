using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using RestSharp.Deserializers;
using RestSharp;

namespace PagerDuty.Net.Tests.Models
{
    [TestClass]
    public class ScheduleEntryTest
    {
        [TestMethod]
        [DeploymentItem("Models\\ScheduleEntry.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "ScheduleEntry.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();

            response.Content = json;

            var myScheduleEntry = ds.Deserialize<ScheduleEntry>(response);

            Assert.IsNotNull(myScheduleEntry);
            Assert.IsNotNull(myScheduleEntry.user);

            Assert.AreEqual(new DateTime(635724000000000000), myScheduleEntry.start);
            Assert.AreEqual(new DateTime(635724432000000000), myScheduleEntry.end);
            Assert.AreEqual("NAEP4DI", myScheduleEntry.user.id);
            Assert.AreEqual("John", myScheduleEntry.user.name);
            Assert.AreEqual("john@contoso.com", myScheduleEntry.user.email);
            Assert.AreEqual("orange", myScheduleEntry.user.color);
        }
    }
}

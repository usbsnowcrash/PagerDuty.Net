using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using RestSharp.Deserializers;
using RestSharp;

namespace PagerDuty.Net.Tests.Models {
    [TestClass]
    public class ScheduleTests {
        [TestMethod]
        [DeploymentItem("Models\\Schedule.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "Schedule.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();

            response.Content = json;

            var mySchedule = ds.Deserialize<Schedule>(response);

            Assert.IsNotNull(mySchedule);
            Assert.IsNotNull(mySchedule.escalation_policies);

            Assert.AreEqual("FS4LEQD", mySchedule.id);
            Assert.AreEqual("24x7 Schedule", mySchedule.name);
            Assert.AreEqual("UTC", mySchedule.time_zone);
            Assert.AreEqual(new DateTime(635726880000000000), mySchedule.today);
            Assert.AreEqual(1, mySchedule.escalation_policies.Count);
            Assert.AreEqual("PAD5HK6", mySchedule.escalation_policies[0].id);
            Assert.AreEqual("Escalation Policy - 24x7", mySchedule.escalation_policies[0].name);
        }
    }
}

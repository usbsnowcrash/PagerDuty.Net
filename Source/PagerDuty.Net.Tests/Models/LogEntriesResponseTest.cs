using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using RestSharp.Deserializers;
using RestSharp;

namespace PagerDuty.Net.Tests.Models {
    [TestClass]
    public class LogEntriesResponseTest {
        [TestMethod]
        [DeploymentItem("Models\\LogEntries.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "LogEntries.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();

            response.Content = json;

            var myLogEntries = ds.Deserialize<LogEntriesResponse>(response);

            Assert.IsNotNull(myLogEntries);

            Assert.AreEqual(3, myLogEntries.log_entries.Count);
            Assert.AreEqual(100, myLogEntries.limit);
            Assert.AreEqual(0, myLogEntries.offset);
            Assert.AreEqual(3, myLogEntries.total);
        }
    }
}

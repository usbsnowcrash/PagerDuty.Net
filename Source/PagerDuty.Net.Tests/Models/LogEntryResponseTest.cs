using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using RestSharp.Deserializers;
using RestSharp;

namespace PagerDuty.Net.Tests.Models {
    [TestClass]
    public class LogEntryResponseTest {
        [TestMethod]
        [DeploymentItem("Models\\LogEntry.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "LogEntry.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();

            response.Content = json;

            var myLogEntry = ds.Deserialize<LogEntryResponse>(response);

            Assert.IsNotNull(myLogEntry);
            Assert.IsNotNull(myLogEntry.log_entry);
            Assert.IsNotNull(myLogEntry.log_entry.agent);
            Assert.IsNotNull(myLogEntry.log_entry.channel);

            Assert.AreEqual("PVPXJJC", myLogEntry.log_entry.id);
            Assert.AreEqual("trigger", myLogEntry.log_entry.type);
            Assert.AreEqual(new DateTime(634981985260000000), myLogEntry.log_entry.created_at);

            Assert.AreEqual("PT23IWX", myLogEntry.log_entry.agent.id);
            Assert.AreEqual("Tim Wright", myLogEntry.log_entry.agent.name);
            Assert.AreEqual("tim@acme.com", myLogEntry.log_entry.agent.email);
            Assert.AreEqual("Eastern Time (US & Canada)", myLogEntry.log_entry.agent.time_zone);
            Assert.AreEqual("purple", myLogEntry.log_entry.agent.color);
            Assert.AreEqual("owner", myLogEntry.log_entry.agent.role);
            Assert.AreEqual("https://secure.gravatar.com/avatar/923a2b907dc04244e9bb5576a42e70a7.png?d=mm&r=PG", myLogEntry.log_entry.agent.avatar_url);
            Assert.AreEqual("/users/PT23IWX", myLogEntry.log_entry.agent.user_url);
            Assert.AreEqual(false, myLogEntry.log_entry.agent.invitation_sent);
            Assert.AreEqual(false, myLogEntry.log_entry.agent.marketing_opt_out);
            Assert.AreEqual("user", myLogEntry.log_entry.agent.type);

            Assert.AreEqual("Martian's are attacking", myLogEntry.log_entry.channel.summary);
            Assert.AreEqual("web_trigger", myLogEntry.log_entry.channel.type);
        }
    }
}

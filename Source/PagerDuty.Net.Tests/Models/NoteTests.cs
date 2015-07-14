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
    public class NoteTests {

        [TestMethod]
        [DeploymentItem("Models\\Note.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "Note.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();
            
            response.Content = json;

            var myAlert = ds.Deserialize<Note>(response);

            Assert.IsNotNull(myAlert);
            Assert.IsNotNull(myAlert.user);

            Assert.AreEqual("22012", myAlert.id);
            Assert.AreEqual(new DateTime(635010512130000000), myAlert.created_at);
            Assert.AreEqual("I caught up to it and told it to slow down.", myAlert.content);
        }

        [TestMethod]
        [DeploymentItem("Models\\Note.json")]
        public void ListJSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "Note.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();

            response.Content = json;

            var myAlert = ds.Deserialize <List<Note>>(response);

            Assert.IsNotNull(myAlert);
            Assert.AreEqual(1, myAlert.Count);
        }
    }
}

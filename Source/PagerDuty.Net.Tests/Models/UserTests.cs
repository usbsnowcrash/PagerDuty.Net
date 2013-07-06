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
    public class UserTests {

        [TestMethod]
        [DeploymentItem("Models\\User.json")]
        public void JSONDeserializationTest() {
            var path = Path.Combine(Environment.CurrentDirectory, "User.json");
            var ds = new JsonDeserializer();
            var response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };

            // Read the file as one string.
            StreamReader myFile = new StreamReader(path);
            string json = myFile.ReadToEnd();
            myFile.Close();

            response.Content = json;

            var myUser = ds.Deserialize<User>(response);

            Assert.IsNotNull(myUser);


            Assert.AreEqual("PT23IWX", myUser.id);
            Assert.AreEqual("Tim Wright", myUser.name);
            Assert.AreEqual("tim@acme.com", myUser.email);
            Assert.AreEqual("Eastern Time (US & Canada)", myUser.time_zone);
            Assert.AreEqual("purple", myUser.color);
            Assert.AreEqual("owner", myUser.role);
            Assert.AreEqual("https://secure.gravatar.com/avatar/923a2b907dc04244e9bb5576a42e70a7.png?d=mm&r=PG", myUser.avatar_url);
            Assert.AreEqual("/users/PT23IWX", myUser.user_url);
            Assert.AreEqual(false, myUser.invitation_sent);
            Assert.AreEqual(false, myUser.marketing_opt_out);
        }
    }
}

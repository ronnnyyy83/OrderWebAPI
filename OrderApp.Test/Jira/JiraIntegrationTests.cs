using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OrderApp.Test.Jira
{
    [TestClass]
    public class JiraIntegrationTests
    {
        private const string baseUrl = "http://localhost:44371/api/jira/";

        //"SAL1005239" should be there for testing purposes//
        [TestMethod]        
        public async Task HttpClient_Should_Get_OKStatus_AND_ITEMS_From_Jira()
        {
            var client = new HttpClient();
            using (var response = await client.GetAsync(baseUrl + "SAL1005239"))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                var responseString = await response.Content.ReadAsStringAsync();
                JObject jRepsonse = JObject.Parse(responseString);

                Assert.AreEqual(2, jRepsonse["total"]);
            }
        }

        [TestMethod]
        public async Task HttpClient_Should_Get_OKStatus_AND_NOT_ITEM_From_Jira()
        {
            var client = new HttpClient();
            using (var response = await client.GetAsync(baseUrl + "0000"))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                var responseString = await response.Content.ReadAsStringAsync();
                JObject jRepsonse = JObject.Parse(responseString);

                Assert.AreEqual(0, jRepsonse["total"]);
            }
        }
    }
}
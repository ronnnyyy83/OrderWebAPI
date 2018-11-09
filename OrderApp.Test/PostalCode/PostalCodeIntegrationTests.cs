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

namespace OrderApp.Test.PostalCode
{
    [TestClass]
    public class PostalCodeIntegrationTests
    {
        private const string baseUrl = "http://localhost:44371/api/postalCode/";

        //"SAL1005239" should be there for testing purposes//
        [TestMethod]        
        public async Task HttpClient_Should_Get_OKStatus_AND_ITEMS_From_PostalCode()
        {
            var client = new HttpClient();
            using (var response = await client.GetAsync(baseUrl + "1186TD/119"))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                var responseString = await response.Content.ReadAsStringAsync();
                JObject jRepsonse = JObject.Parse(responseString);

                Assert.AreEqual("Praam", jRepsonse["_embedded"]["addresses"][0]["street"]);
                Assert.AreEqual("Amstelveen", jRepsonse["_embedded"]["addresses"][0]["city"]["label"]);
            }
        }

        [TestMethod]
        public async Task HttpClient_Should_Get_OKStatus_AND_NO_ITEM_From_PostalCode()
        {
            var client = new HttpClient();
            using (var response = await client.GetAsync(baseUrl + "1186TD/1000"))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                var responseString = await response.Content.ReadAsStringAsync();
                JObject jRepsonse = JObject.Parse(responseString);

                Assert.IsFalse(jRepsonse["_embedded"]["addresses"].HasValues);
            }
        }

        [TestMethod]
        public async Task HttpClient_Should_Get_BADREQUEST_From_PostalCode()
        {
            var client = new HttpClient();
            using (var response = await client.GetAsync(baseUrl + "1000/10"))
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
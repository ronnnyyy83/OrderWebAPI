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

namespace OrderApp.Test.Order
{
    [TestClass]
    public class OrderIntegrationTests
    {
        private const string baseUrl = "http://localhost:44371/api/order/";
        [TestMethod]
        public async Task HttpClient_Should_Get_OKStatus_From_Orders()
        {
            var client = new HttpClient();
            using (var response = await client.GetAsync(baseUrl))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        //OrderId 1 should not be deleted for testing purposes//
        [TestMethod]
        public async Task HttpClient_Should_Get_OKStatus_From_Order_Valid_Id()
        {
            var client = new HttpClient();
            using (var response = await client.GetAsync(baseUrl + "1"))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task HttpClient_Should_Get_NotFoundStatus_From_Order_Not_Valid_Id()
        {
            var client = new HttpClient();
            using (var response = await client.GetAsync(baseUrl + "0"))
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task HttpClient_Should_GET_NoContentStatus_From_UpdateOrder_Valid_Id()
        {
            var client = new HttpClient();
            var item = "{\"orderId\":1,\"orderNo\":\"SAL1005239\",\"price\":122.10,\"name\":\"testname\",\"lastName\":\"testlast\",\"postCode\":\"1186TD\",\"houseNumber\":\"119\",\"street\":\"Praam\",\"city\":\"Amstelveen\"},";
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(baseUrl + "1"),
                Method = HttpMethod.Put,
                Content = new StringContent(item, Encoding.UTF8, "application/json")
            };

            using (var response = await client.SendAsync(request))
            {
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task HttpClient_Should_GET_NotFoundStatus_From_UpdateOrder_Not_Valid_Id()
        {
            var client = new HttpClient();
            var item = "{\"orderId\":0,\"orderNo\":\"SAL1005239\",\"price\":122.10,\"name\":\"testname\",\"lastName\":\"testlast\",\"postCode\":\"1186TD\",\"houseNumber\":\"119\",\"street\":\"Praam\",\"city\":\"Amstelveen\"},";
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(baseUrl + "0"),
                Method = HttpMethod.Put,
                Content = new StringContent(item, Encoding.UTF8, "application/json")
            };

            using (var response = await client.SendAsync(request))
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task HttpClient_Should_GET_OKStatus_From_CreateOrder()
        {
            var orderNo = Guid.NewGuid().ToString();
            var item = "{\"orderNo\":\"" + orderNo + "\",\"price\":155.1,\"name\":\"testname\",\"lastName\":\"testlast\",\"postCode\":\"1186TD\",\"houseNumber\":\"119\",\"street\":\"Praam\",\"city\":\"Amstelveen\"}";
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(baseUrl),
                Method = HttpMethod.Post,
                Content = new StringContent(item, Encoding.UTF8, "application/json")
            };

            using (var response = await client.SendAsync(request))
            {
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                var responseString = await response.Content.ReadAsStringAsync();
                JObject jRepsonse = JObject.Parse(responseString);
                jRepsonse.Property("orderId").Remove();
                var result = Regex.Replace(jRepsonse.ToString(), @"\t|\n|\r", "");
                Assert.AreEqual(item.Trim(), result.Replace(" ", ""));
            }
        }

        [TestMethod]
        public async Task HttpClient_Should_GET_FAILS_From_CreateOrder_With_Existing_Order_No()
        {
            var orderNo = "00000000-0000-0000-0000-000000000000";
            var item = "{\"orderNo\":\"" + orderNo + "\",\"price\":155.1,\"name\":\"testname\",\"lastName\":\"testlast\",\"postCode\":\"1186TD\",\"houseNumber\":\"119\",\"street\":\"Praam\",\"city\":\"Amstelveen\"}";
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(baseUrl),
                Method = HttpMethod.Post,
                Content = new StringContent(item, Encoding.UTF8, "application/json")
            };
            
            using (var response = await client.SendAsync(request))
            {
                Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task HttpClient_Should_GET_BADREQUEST_From_CreateOrder_With_TooLong_PostCode()
        {
            var orderNo = Guid.NewGuid().ToString();
            var item = "{\"orderNo\":\"" + orderNo + "\",\"price\":155.1,\"name\":\"testname\",\"lastName\":\"testlast\",\"postCode\":\"1186334343TD\",\"houseNumber\":\"119\",\"street\":\"Praam\",\"city\":\"Amstelveen\"}";
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(baseUrl),
                Method = HttpMethod.Post,
                Content = new StringContent(item, Encoding.UTF8, "application/json")
            };

            using (var response = await client.SendAsync(request))
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
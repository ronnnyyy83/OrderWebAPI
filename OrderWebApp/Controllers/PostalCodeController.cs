using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrderWebApp.Helpers;
using OrderWebApp.Models;

namespace OrderWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostalCodeController : ControllerBase
    {
        private IConfiguration _config;

        public PostalCodeController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("{postCode}/{houseNumber}")]
        public IActionResult GetPostalCode(string postCode, string houseNumber)
        {
            var EndPoint = _config.GetValue<string>("MySettings:PostalCodeURL") + postCode + "&number=" + houseNumber;
            WebHeaderCollection Headers = new WebHeaderCollection();
            Headers.Add("accept", "application/hal+json");
            Headers.Add("x-api-key", _config.GetValue<string>("MySettings:PostalCodeAPIKey"));

            WebRequestModel requestResponse = WebRequestHelper.MakeRequest(EndPoint, Headers, null);

            if (requestResponse.StatusCode == (int)HttpStatusCode.OK)
            {
                return Ok(requestResponse.Response);
            }
            else
            {
                return StatusCode(requestResponse.StatusCode, requestResponse.StatusDescription);
            }
        }
    }
}
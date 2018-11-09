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
    public class JiraController : ControllerBase
    {
        private IConfiguration _config;

        public JiraController(IConfiguration config)
        {            
            _config = config;
        }

        [HttpGet("{orderNo}")]
        public IActionResult GetPostalCode(string orderNo)
        {
            var EndPoint = _config.GetValue<string>("MySettings:JiraURL");
            var Data = "{\"jql\": \"description ~'" + orderNo + "'\"}"; 
            WebRequestModel requestResponse = WebRequestHelper.MakeRequest(EndPoint, null, Data, "POST");

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
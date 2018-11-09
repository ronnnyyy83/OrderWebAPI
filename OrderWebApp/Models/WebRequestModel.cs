using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderWebApp.Models
{
    public class WebRequestModel
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Response { get; set; }

        public WebRequestModel(int _statusCode, string _statusDescription, string _response) {
            StatusCode = _statusCode;
            StatusDescription = _statusDescription;
            Response = _response;
        }
    }
}

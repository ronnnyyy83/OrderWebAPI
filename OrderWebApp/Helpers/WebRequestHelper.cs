using OrderWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderWebApp.Helpers
{
    public static class WebRequestHelper
    {
        public static WebRequestModel MakeRequest(string EndPoint, WebHeaderCollection Headers, string Data, string Method = "GET", string ContentType = "application/Json") {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint);

            request.Method = Method;
            request.ContentType = ContentType;
            request.ContentLength = 0;

            if (Headers != null && Headers.Count > 0)
            {
                request.Headers = Headers;
            }

            if (Method == "POST" && Data != null && Data.Length > 0) {
                request.ContentLength = Data.Length;

                using (Stream webStream = request.GetRequestStream())
                using (StreamWriter requestWriter = new StreamWriter(webStream))
                {
                    requestWriter.Write(Data);
                }
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return new WebRequestModel((int)response.StatusCode, response.StatusDescription, null);
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }

                    return new WebRequestModel((int)response.StatusCode, response.StatusDescription, responseValue);
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse exResponse = (HttpWebResponse)ex.Response;
                return new WebRequestModel((int)exResponse.StatusCode, exResponse.StatusDescription, null);
            }
            catch (Exception ex)
            {
                return new WebRequestModel(500, "Internal Server Error", null);
            }
        }    
    }
}

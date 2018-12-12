using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace APIService.Models
{
    public class ClientRequest
    {
        public string endPoint { get; set; }

        public string MakeRequest()
        {
            string GET = "GET";
            string resp = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = GET;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("error code:  " + response.StatusCode.ToString());
                }


                using (Stream streamResponse = response.GetResponseStream())
                {
                    if (streamResponse != null)
                    {
                        using (StreamReader reader = new StreamReader(streamResponse))
                        {
                            resp = reader.ReadToEnd();
                        }
                    }
                }

                return resp;
            }
        }
    }
}
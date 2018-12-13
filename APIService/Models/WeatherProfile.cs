using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Json;

namespace APIService.Models
{
    public class WeatherProfile
    {
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
        public int Temp { get; set; }
        public string URI { get; set; }
        public string Response { get; set; }
        public string City { get; set; }

        public string apiCall = "http://api.openweathermap.org/data/2.5/weather";

        public int CollectTemp()
        {
            BuildURI();
            FindTemp(Response);
            return Temp;
        }
        public string CollectCity()
        {
            FindCity(Response);
            return City;
        }
        public void BuildURI()
        {
            string addZipCountry = "?zip=" + ZipCode + "," + CountryCode;
            string addUnit = "&units=imperial";
            string key = "&appid=2700d650159919f352b27e0c318d5beb";

            URI = apiCall + addZipCountry + addUnit + key;

            Send(URI);
        }
        public void Send(string endPoint)
        {
            endPoint = URI;
            ClientRequest client = new ClientRequest();
            client.endPoint = endPoint;
            Response = client.MakeRequest();
        }
        public void FindCity(string data)
        {
            int i = 0;
            bool flag = false;

            while (i < data.Length)
            {
                if(data.Substring(i,6) == "\"name\"")
                {
                    i += 8;
                    while(data[i] != '"')
                    {
                        City += data[i];
                        i++;
                        flag = true;
                    }
                    if (flag)
                        break;
                }
                i++;
            }
        }
        public void FindTemp(string data)
        {
            int i = 0;
            string cutTemp = string.Empty;
            int count = 0;

            while (i < data.Length)
            {
                if(data.Substring(i,4) == "temp")
                {  
                    i += 4;
                    while(count < 2)
                    { 
                        if(char.IsDigit(data[i]))
                        {
                            cutTemp += data[i];
                            count++;
                        }   
                        i++;
                    } 
                }
                if (count == 2)
                {
                    break;
                }
                i++;
            }
            Temp = Convert.ToInt32(cutTemp);
        }
    }
}
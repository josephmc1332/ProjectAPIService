using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIService.Models
{
    public class WeatherProfile
    {
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
          
        

        public string apiCall = "http://api.openweathermap.org/data/2.5/weather";
        public string complete = string.Empty;

        public int BuildURI()
        {
            string addZipCountry = "?zip=" + ZipCode + "," + CountryCode;
            string addUnit = "&units=imperial";
            string key = "&appid=2700d650159919f352b27e0c318d5beb";

            complete = apiCall + addZipCountry + addUnit + key;

            return Convert.ToInt32(Send(complete));
        }
        public string Send(string endPoint)
        {
            endPoint = complete;
            ClientRequest client = new ClientRequest();
            client.endPoint = endPoint;

            return FindTemp(client.MakeRequest());
        }
        public string FindTemp(string temp)
        {
            int i = 0;
            string cutTemp = string.Empty;
            char[] charray = new char[temp.Length];
            charray = temp.ToCharArray();
            int count = 0;

            while (i < temp.Length)
            {
                if(temp.Substring(i,4) == "temp")
                {
                    
                    i += 4;
                    while(count < 2)
                    { 
                        if(char.IsDigit(charray[i]))
                        {
                            cutTemp += charray[i];
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
            return cutTemp;
        }
    }
}
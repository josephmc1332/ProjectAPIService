using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APIService.Models;
using Newtonsoft.Json;

namespace APIService.Models
{
    public class FoodModel
    {
        public string Eat { get; set; }
        public int Helper { get; set; } = 1;
        public string City { get; set; }
        public string EndPoint { get; set; } = "https://query.yahooapis.com/v1/public/yql?q=";
        public string jsonText { get; set; }
        public Dictionary<string,string> Summary = new Dictionary<string, string>();

        /// <summary>
        /// Returns the JSON format string from the call to API endpoint
        /// </summary>
        /// <returns></returns>
        public string Call()
        {
            string complete = string.Empty;

            complete = EndPoint + $"select * from local.search where query=\"{Eat}\" and location=\"{City}\"&format=json";

            ClientRequest client = new ClientRequest();
            client.EndPoint = complete;
            jsonText = client.MakeRequest();
            Summary = FindName();
            return " ";
        }
        /// <summary>
        /// Returns a Queue that is filled with properties from FoodModel class
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public Queue<string> FillQ(Queue<string> q)
        {
            q.Enqueue("Title");
            q.Enqueue("Address");
            q.Enqueue("City");
            q.Enqueue("State");
            q.Enqueue("Phone");
            return q;
        }
        /// <summary>
        /// Returns a Dictionary that contains elements from API query. Key will match properties incremented by 1 from FoodModel class.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> FindName()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            //This queue will continue to refill until we have up to 7 complete results
            Queue<string> q = new Queue<string>();
            //laps will track the number of complete results that are obtained throughout the loop
            int laps = 1;
            //passes JSON format string from API call to a local variable
            string a = jsonText;
            //this string will build until it has one element, such as for Title : "Omni Health and Fitness". Then it will reset
            string name = string.Empty;
            //Initial fill for local queue
            q = FillQ(q);

            //until the end of JSON string is reached
            for (int i = 0; i < a.Length; i++)
            {
                //this is to protect the bounds of the string.
                if (a.Length - q.Peek().Length <= i)
                    break;

                //will find the property by checking for a verbatim match
                if(a.Substring(i,q.Peek().Length) == q.Peek())
                {
                    //because of the format of JSON string, we know that once we have a match with property we can jump 
                    //the iterator by the length of property name as well as skipping over ":" key value syntax
                    i += (q.Peek().Length + 3);
                    //finding a " will indicate that we have retrieved the complete current result
                    while(a[i] != '"')
                    {
                        //the build/rebuild string
                        name += a[i];
                        i++;
                    }
                    //functions to perform once the property has successfully been retrieved
                    if(a[i] == '"')
                    {
                        //along with storing the new value this will pop of the current Peek of local queue
                        //"Title1" : "Omni Health and Fitness"
                        d.Add(q.Dequeue() + laps.ToString(), name);
                        //since I only want 7 complete results I only refill until it reaches that point
                        if (q.Count == 0 && laps < 7)
                        {
                            q = FillQ(q);
                            laps++;
                        }
                        //if laps is at 7 then I know I have no need to keep looking through JSON string
                        else if (laps > 7)
                            break;
                        else if (q.Count == 0)
                            break;
                        name = string.Empty;
                    }
                }
            }
            return d;           
        }
    }
}
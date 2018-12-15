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
        public Queue<string> FillQ(Queue<string> q)
        {
            q.Enqueue("Title");
            q.Enqueue("Address");
            q.Enqueue("City");
            q.Enqueue("State");
            q.Enqueue("Phone");
            return q;
        }
        public Dictionary<string,string> FindName()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            Queue<string> q = new Queue<string>();
            int laps = 1;
            string a = jsonText;
            string name = string.Empty;
            q = FillQ(q);

            for (int i = 0; i < a.Length; i++)
            {
                if (a.Length - q.Peek().Length <= i)
                    break;

                if(a.Substring(i,q.Peek().Length) == q.Peek())
                {
                    i += (q.Peek().Length + 3);
                    while(a[i] != '"')
                    {
                        name += a[i];
                        i++;
                    }
                    if(a[i] == '"')
                    {
                        d.Add(q.Dequeue() + laps.ToString(), name);
                        if (q.Count == 0 && laps < 7)
                        {
                            q = FillQ(q);
                            laps++;
                        }
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
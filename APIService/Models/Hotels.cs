using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIService.Models
{
    public class HotelModel
    {
        public string Query { get; set; } 
        public string UserCity { get; set; }
        public int Counter { get; set; } = 1;
        public Dictionary<string, string> d = new Dictionary<string, string>();

        public void BuildURI()
        {
            string send = $"http://query.yahooapis.com/v1/public/yql?q=select * from local.search where query=\"hotels\" and location=\"{UserCity}\"&format=json";
            ClientRequest client = new ClientRequest
            {
                EndPoint = send
            };
            Query = client.MakeRequest();
        }
        public Queue<string> FillQ()
        {
            Queue<string> q = new Queue<string>();
            q.Enqueue("Title");
            q.Enqueue("Address");
            q.Enqueue("City");
            q.Enqueue("State");
            q.Enqueue("Phone");
            q.Enqueue("Distance");

            return q;
        }
        public string FindHotel()
        {
            BuildURI();
            Queue<string> q = new Queue<string>();
            q = FillQ();
            int i = 0;
            int lap = 1;
            string attr = string.Empty;
            while(i < Query.Length)
            {
                if(Query.Length - i < q.Peek().Length)
                {
                    break;
                }
                if(Query.Substring(i,q.Peek().Length) == q.Peek())
                {
                    i += q.Peek().Length + 3;
                    while(Query[i] != '"')
                    {
                        attr += Query[i];
                        i++;
                    }
                    d.Add(lap.ToString() + q.Dequeue(), attr);
                    attr = string.Empty;
                }
                if(q.Count == 0 && lap <= 7)
                {
                    lap++;
                    q = FillQ();
                }
                else if(lap > 7)
                {
                    break;
                }
                i++;
            }
            return " ";
        }

        #region Needs Work
        //public string FindHotels()
        //{
        //    Queue<string> q = new Queue<string>();
        //    BuildURI();

        //    FillQ(ref q);
        //    for (int i = 0; i < Query.Length; i++)
        //    {
        //        if(Query[i] == '[')
        //        {
        //            HotelModel hotel = new HotelModel();
        //            while(Query[i] != ']')
        //            {

        //                if (Query.Substring(i,q.Peek().Length)==q.Peek())
        //                {
        //                    hotel.Title = hotel.Title == null && q.Peek() == "Title" ? Parse(ref i, q.Dequeue().Length) : hotel.Title;

        //                    hotel.Address = hotel.Address == null && q.Peek() == "Address"? Parse(ref i, q.Dequeue().Length) : hotel.Address;

        //                    hotel.Address = hotel.City == null && q.Peek() == "City" ? Parse(ref i, q.Dequeue().Length) : hotel.City;

        //                    hotel.State = hotel.State == null && q.Peek() == "State" ? Parse(ref i, q.Dequeue().Length) : hotel.State;

        //                    hotel.Address = hotel.Phone == null && q.Peek() == "Phone" ? Parse(ref i, q.Dequeue().Length) : hotel.Phone;

        //                    hotel.Distance = hotel.Distance == null && q.Peek() == "Distance" ? Parse(ref i, q.Dequeue().Length) : hotel.Phone;

        //                    hotel.Link = hotel.Link == null && q.Peek() == "BusinessClickUrl" ? Parse(ref i, q.Dequeue().Length) : hotel.Link;
        //                }
        //                i++;
        //            }
        //            hotels.Add(hotel);
        //            if(q.Count == 0)
        //            {
        //                FillQ(ref q);
        //            }
        //            if (Query.Length - i < q.Peek().Length)
        //                break;
        //        }

        //    }
        //    return " ";

        //}
        

        //public string Parse(ref int i, int len)
        //{
        //    string a = string.Empty;
        //    i = len + 3;
        //    while(Query[i] != '"')
        //    {
        //        a += Query[i];
        //    }
        //    return a;
        //}
        #endregion
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIService.Models;

namespace APIService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Check()
        {
            ViewBag.Title = "Check";

            return View();
        }
        [HttpPost]
        public ViewResult Check(ClientRequest client)
        {
            ViewBag.Title = "Response";


            return View("Response", client);
        }
       [HttpGet]
        public ViewResult Weather()
        {
            ViewBag.Title = "Weather";

            return View();
        }
       [HttpPost]
        public ViewResult Weather(WeatherProfile profile)
        {
            ViewBag.Title = "Report";

            return View("Report", profile);
        }
        [HttpGet]
        public ViewResult FindFood()
        {

            return View();
        }
      
        public ViewResult FindFood(FoodModel food)
        {

            return View("FoodDetails", food);
        }
    }
}
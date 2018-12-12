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
            ViewBag.Title = "Check";


            return View("Response", client);
        }
    }
}
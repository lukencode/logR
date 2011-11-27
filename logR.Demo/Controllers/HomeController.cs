using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace logR.Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            LogR.Log("HomeController", Level.Error, "Oh noes error!");
            return View();
        }

        public ActionResult About()
        {
            LogR.Log("HomeController", Level.Info, "WENT TO ABOUT PAGE");
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using logR.Demo.Models;

namespace logR.Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            var model = new HomeViewModel();
            model.Levels = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = Level.Trace.ToString(), Value = ((int)Level.Trace).ToString() },
                    new SelectListItem() { Text = Level.Debug.ToString(), Value = ((int)Level.Debug).ToString() },
                    new SelectListItem() { Text = Level.Info.ToString(), Value = ((int)Level.Info).ToString() },
                    new SelectListItem() { Text = Level.Warn.ToString(), Value = ((int)Level.Warn).ToString() },
                    new SelectListItem() { Text = Level.Error.ToString(), Value = ((int)Level.Error).ToString() },
                    new SelectListItem() { Text = Level.Fatal.ToString(), Value = ((int)Level.Fatal).ToString() }
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                LogR.Log("HomeController", (Level)Convert.ToInt32(model.SelectedLevel), model.Message);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult About()
        {
            LogR.Log("HomeController", Level.Info, "WENT TO ABOUT PAGE");
            return View();
        }
    }
}

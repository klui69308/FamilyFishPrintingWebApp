using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FamilyFishMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadyToBuy()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ReadyToPrint()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult BringYourOwn()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
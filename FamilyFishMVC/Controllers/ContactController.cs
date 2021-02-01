using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FamilyFishMVC.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            string mapKey = ConfigurationManager.AppSettings["mapKey"];
            string mapAddress = "6121 N Hanley Rd, Berkeley, MO 63134";
            string mapSrc = "https://www.google.com/maps/embed/v1/place?key=" +
                         mapKey + "&q=" + HttpUtility.UrlEncode(mapAddress);
            ViewBag.mapSrc = mapSrc;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMessage(string name, string email, string message)
        {
            var subject = $@"Customer '{name}' sent us an inqury";
            var body = "Email: " + email + "." + Environment.NewLine + message; 

            MessageSender.SendEmail("familyfishprinting@gmail.com", subject, body);
            return RedirectToAction("Index");
        }
    }
}
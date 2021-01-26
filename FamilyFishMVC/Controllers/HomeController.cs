using FamilyFishMVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FamilyFishMVC.Controllers
{
    public class HomeController : Controller
    {
        private FamilyFishDBEntities db = new FamilyFishDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            var userId = User.Identity.GetUserId();
            var customer = db.Customers.Find(userId);
            if(customer != null)
            {
                ViewBag.customer = customer.Fname;
            }
            else
            {
                ViewBag.customer = "Admin";
            }
            return PartialView();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
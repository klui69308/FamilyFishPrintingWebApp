using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using FamilyFishMVC.Models;
using Microsoft.AspNet.Identity;

namespace FamilyFishMVC.Controllers
{
    public class CustomersController : Controller
    {
        private FamilyFishDBEntities db = new FamilyFishDBEntities();

        // GET: Customers
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //var customers = db.Customers.Include(c => c.AspNetUser);
            var customers = db.Customers;
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            Customer customer = new Customer
            {
                Id = User.Identity.GetUserId()
            };
            //ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View(customer);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Fname,Lname,Mname,Email,StreetNum,StreetName,City,State,Zip,HPhone,CPhone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if(customer.Id != User.Identity.GetUserId())
                {
                    throw new InvalidOperationException("User ID is not loggedin user");
                }
                db.Customers.Add(customer);
                db.SaveChanges();

                var subject = "Welcome to Family Fish Printing";
                var body = "Hi, " + customer.Fname +"!" + Environment.NewLine +
                           "Thank you for registering. You can now buy the best custom printed T-Shirt." + Environment.NewLine +
                           "Feel free to contact us at familyfishprint@gmail.com for any questions." + Environment.NewLine +
                           "Enjoy your stay and have fun.";

                MessageSender.SendEmail(customer.Email, subject, body);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", customer.Id);
            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", customer.Id);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fname,Lname,Mname,Email,StreetNum,StreetName,City,State,Zip,HPhone,CPhone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", customer.Id);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            try
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            catch
            {
                string alertMessage = "There is an active cart for this customer";
                ModelState.AddModelError("", alertMessage);
                return View(customer);
            }
            return RedirectToAction("Index");
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

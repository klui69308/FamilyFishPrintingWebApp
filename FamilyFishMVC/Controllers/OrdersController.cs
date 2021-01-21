using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FamilyFishMVC.Models;
using Microsoft.AspNet.Identity;

namespace FamilyFishMVC.Controllers
{
    public class OrdersController : Controller
    {
        private FamilyFishDBEntities db = new FamilyFishDBEntities();
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            var customerId = User.Identity.GetUserId();
            var order = db.Orders.FirstOrDefault(o=>o.Cid == customerId && o.SubmittedDate == null);
            if (order == null)
            {
                order = new Order
                {
                    Cid = customerId
                };
                db.Orders.Add(order);
            }
            var orderLineItem = order.OrderLineItems.FirstOrDefault(item => item.Pid == id);
            var product = db.Products.Find(id);
            if (orderLineItem != null)
            {
                orderLineItem.Quantity++;
                db.Entry(orderLineItem).State = EntityState.Modified;
            }
            else
            {
                orderLineItem = new OrderLineItem
                {
                    Pid = id,
                    Quantity = 1,
                    Price = product.Price
                };
                order.OrderLineItems.Add(orderLineItem);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Submit(int id)
        {
            var order = db.Orders.Find(id);
            order.SubmittedDate = DateTime.Now;
            foreach(var item in order.OrderLineItems)
            {
                item.Product.InventoryCount -= item.Quantity;
                db.Entry(item.Product).State = EntityState.Modified;
            }
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Orders
        public ActionResult Index(int? id)
        {
            Order order;
            if (id is null)
            {
                var customerId = User.Identity.GetUserId();
                order = db.Orders.FirstOrDefault(o => o.Cid == customerId && o.SubmittedDate == null);
            }
            else
            {
                order = db.Orders.Find(id);
            }
            var orderLineItem = order?.OrderLineItems;
            
            if(orderLineItem is null)
            {
                orderLineItem = new List<OrderLineItem>();
            }
            
            return View(orderLineItem.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.Cid = new SelectList(db.Customers, "Id", "Fname");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cid")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cid = new SelectList(db.Customers, "Id", "Fname", order.Cid);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cid = new SelectList(db.Customers, "Id", "Fname", order.Cid);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cid")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cid = new SelectList(db.Customers, "Id", "Fname", order.Cid);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
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

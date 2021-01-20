using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FamilyFishMVC.Models;

namespace FamilyFishMVC.Controllers
{
    public class OrderLineItemsController : Controller
    {
        private FamilyFishDBEntities db = new FamilyFishDBEntities();

        // GET: OrderLineItems
        public ActionResult Index()
        {
            var orderLineItems = db.OrderLineItems.Include(o => o.Order).Include(o => o.Product);
            return View(orderLineItems.ToList());
        }

        // GET: OrderLineItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLineItem orderLineItem = db.OrderLineItems.Find(id);
            if (orderLineItem == null)
            {
                return HttpNotFound();
            }
            return View(orderLineItem);
        }

        // GET: OrderLineItems/Create
        public ActionResult Create()
        {
            ViewBag.Oid = new SelectList(db.Orders, "Id", "Cid");
            ViewBag.Pid = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: OrderLineItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Oid,Pid,Quantity,Price")] OrderLineItem orderLineItem)
        {
            if (ModelState.IsValid)
            {
                db.OrderLineItems.Add(orderLineItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Oid = new SelectList(db.Orders, "Id", "Cid", orderLineItem.Oid);
            ViewBag.Pid = new SelectList(db.Products, "Id", "Name", orderLineItem.Pid);
            return View(orderLineItem);
        }

        // GET: OrderLineItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLineItem orderLineItem = db.OrderLineItems.Find(id);
            if (orderLineItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Oid = new SelectList(db.Orders, "Id", "Cid", orderLineItem.Oid);
            ViewBag.Pid = new SelectList(db.Products, "Id", "Name", orderLineItem.Pid);
            return View(orderLineItem);
        }

        // POST: OrderLineItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Oid,Pid,Quantity,Price")] OrderLineItem orderLineItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderLineItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Orders");
            }
            ViewBag.Oid = new SelectList(db.Orders, "Id", "Cid", orderLineItem.Oid);
            ViewBag.Pid = new SelectList(db.Products, "Id", "Name", orderLineItem.Pid);
            return RedirectToAction("Index", "Orders");
        }

        // GET: OrderLineItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLineItem orderLineItem = db.OrderLineItems.Find(id);
            if (orderLineItem == null)
            {
                return HttpNotFound();
            }
            return View(orderLineItem);
        }

        // POST: OrderLineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderLineItem orderLineItem = db.OrderLineItems.Find(id);
            db.OrderLineItems.Remove(orderLineItem);
            db.SaveChanges();
            return RedirectToAction("Index", "Orders");
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

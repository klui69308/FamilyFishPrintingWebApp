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
    public class ProductsController : Controller
    {
        private FamilyFishDBEntities db = new FamilyFishDBEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ProductImages productImage = new ProductImages
            {
                ImageId = product.Id,
                Name = product.Name
            };
            productImage.ImageId = product.Id;
            productImage.Name = product.Name;
            string imagePath = "~/ProductImages/" + "stockPhoto" + product.Id + ".png";
            try
            {
                productImage.Image = imagePath;
            }
            catch
            {
                productImage.Image = "~/ProductImages/yourPhotoHere.jpg";
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.allCategories = new MultiSelectList(db.Categories.ToList(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProducViewModels productViewModels, HttpPostedFileBase uploadFile)
        {
            var isValid = ModelState.IsValid;
            if (isValid && uploadFile != null && uploadFile.ContentLength > 0 && !uploadFile.FileName.EndsWith(".png"))
            {
                ModelState.AddModelError("", "File must be a png tpye!");
                isValid = false;
            }
            if (isValid)
            {
                var product = new Product
                {
                    Detail = productViewModels.Detail,
                    Name = productViewModels.Name,
                    InventoryCount = productViewModels.InventoryCount,
                    ReservedCount = productViewModels.ReservedCount,
                    Price = productViewModels.Price
                };
                foreach (var item in productViewModels.CategoryIds)
                {
                    var category = db.Categories.Find(item);
                    product.Categories.Add(category);
                }
                db.Products.Add(product);
                db.SaveChanges();
                if(uploadFile!= null && uploadFile.ContentLength > 0)
                {
                    var filePath = Server.MapPath(product.GetImagePath(HttpContext, false));
                    uploadFile.SaveAs(filePath);
                }
                return RedirectToAction("Index");
            }
            ViewBag.allCategories = new MultiSelectList(db.Categories.ToList(), "Id", "Name");
            return View(productViewModels);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Detail,InventoryCount,ReservedCount,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            product.Categories.Clear();
            db.SaveChanges();
            db.Products.Remove(product);
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

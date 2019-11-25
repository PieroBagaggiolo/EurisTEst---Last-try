using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURISTest.Models;

namespace EURISTest.Controllers
{
    public class ProductCatalogController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        //
        // GET: /ProductCatalog/

        public ActionResult Index()
        {
            var productcatalogs = db.ProductCatalogs.Include(p => p.Catalog).Include(p => p.Product);
            return View(productcatalogs.ToList());
        }

        //
        // GET: /ProductCatalog/Details/5

        public ActionResult Details(int id = 0)
        {
            ProductCatalog productcatalog = db.ProductCatalogs.Find(id);
            if (productcatalog == null)
            {
                return HttpNotFound();
            }
            return View(productcatalog);
        }

        //
        // GET: /ProductCatalog/Create

        public ActionResult Create()
        {
            ViewBag.FKCatalogId = new SelectList(db.Catalogs, "Id", "Description");
            ViewBag.FKProductId = new SelectList(db.Products, "Id", "Code");
            return View();
        }

        //
        // POST: /ProductCatalog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCatalog productcatalog)
        {
            if (ModelState.IsValid)
            {
                db.ProductCatalogs.Add(productcatalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKCatalogId = new SelectList(db.Catalogs, "Id", "Description", productcatalog.FKCatalogId);
            ViewBag.FKProductId = new SelectList(db.Products, "Id", "Code", productcatalog.FKProductId);
            return View(productcatalog);
        }

        //
        // GET: /ProductCatalog/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProductCatalog productcatalog = db.ProductCatalogs.Find(id);
            if (productcatalog == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKCatalogId = new SelectList(db.Catalogs, "Id", "Description", productcatalog.FKCatalogId);
            ViewBag.FKProductId = new SelectList(db.Products, "Id", "Code", productcatalog.FKProductId);
            return View(productcatalog);
        }

        //
        // POST: /ProductCatalog/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCatalog productcatalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productcatalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKCatalogId = new SelectList(db.Catalogs, "Id", "Description", productcatalog.FKCatalogId);
            ViewBag.FKProductId = new SelectList(db.Products, "Id", "Code", productcatalog.FKProductId);
            return View(productcatalog);
        }

        //
        // GET: /ProductCatalog/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProductCatalog productcatalog = db.ProductCatalogs.Find(id);
            if (productcatalog == null)
            {
                return HttpNotFound();
            }
            return View(productcatalog);
        }

        //
        // POST: /ProductCatalog/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCatalog productcatalog = db.ProductCatalogs.Find(id);
            db.ProductCatalogs.Remove(productcatalog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
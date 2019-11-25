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
    public class CatalogController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        //
        // GET: /Catalog/

        public ActionResult Index()
        {
            return View(db.Catalogs.ToList());
        }

        //
        // GET: /Catalog/Details/5

        public ActionResult Details(int id = 0)
        {
            CatalogModel catalogmodel = db.Catalogs.Find(id);
            if (catalogmodel == null)
            {
                return HttpNotFound();
            }
            return View(catalogmodel);
        }

        //
        // GET: /Catalog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Catalog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CatalogModel catalogmodel)
        {
            if (ModelState.IsValid)
            {
                db.Catalogs.Add(catalogmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catalogmodel);
        }

        //
        // GET: /Catalog/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CatalogModel catalogmodel = db.Catalogs.Find(id);
            if (catalogmodel == null)
            {
                return HttpNotFound();
            }
            return View(catalogmodel);
        }

        //
        // POST: /Catalog/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CatalogModel catalogmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalogmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catalogmodel);
        }

        //
        // GET: /Catalog/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CatalogModel catalogmodel = db.Catalogs.Find(id);
            if (catalogmodel == null)
            {
                return HttpNotFound();
            }
            return View(catalogmodel);
        }

        //
        // POST: /Catalog/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CatalogModel catalogmodel = db.Catalogs.Find(id);
            db.Catalogs.Remove(catalogmodel);
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
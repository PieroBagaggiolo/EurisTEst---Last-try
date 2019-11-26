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

        public ActionResult Index(string sortOrder, string search)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = sortOrder == "description" ? "description" : "description";
            var catalogs = from c in db.Catalogs
                           select c;
            if (!String.IsNullOrEmpty(search))
            {
                catalogs = catalogs.Where(s => s.Description.Contains(search));
            }
            switch (sortOrder)
            {
                case "id_desc":
                    catalogs = catalogs.OrderByDescending(p => p.Description);
                    break;
                case "description":
                    catalogs = catalogs.OrderBy(p => p.Description);
                    break;
            }
            catalogs = catalogs.OrderBy(c => c.Description);
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
            List<ProductCatalog> listCatalog = new List<ProductCatalog>();
            foreach(var item in db.ProductCatalogs.Include(x=>x.Catalog).Include(x=>x.Product))
            {
                if(catalogmodel.Id == item.FKCatalogId)
                {
                    listCatalog.Add(item);
                }
            }
            
            return View(listCatalog);
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
        public ActionResult AddProduct(int id = 0)
        {
            
            CatalogModel catalogmodel = db.Catalogs.Find(id);
            if (catalogmodel == null)
            {
                return HttpNotFound();
            }
            List<CatalogModel> listCatalog = new List<CatalogModel>();
            listCatalog.Add(catalogmodel);
            
            ViewBag.FKCatalogId = new SelectList(listCatalog, "id", "Description");
            ViewBag.FKProductId = new SelectList(db.Products, "Id", "Description");
            return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductCatalog productcatalog)
        {
            if (ModelState.IsValid)
            {
                db.ProductCatalogs.Add(productcatalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKCatalogId = new SelectList(db.Catalogs, "Id", "Description", productcatalog.FKCatalogId);
            ViewBag.FKProductId = new SelectList(db.Products, "Id", "Description", productcatalog.FKProductId);
            return View(productcatalog);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
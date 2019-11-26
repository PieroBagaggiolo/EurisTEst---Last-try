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
    public class ProductController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        //
        // GET: /Product/

        public ActionResult Index(string sortOrder, string search)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewBag.NameSortParm = sortOrder == "description" ? "description" : "description";

            var products = from p in db.Products
                           select p;

            if(!String.IsNullOrEmpty(search))
            {
                products = products.Where(s => s.Code.Contains(search) || s.Description.Contains(search));
            }

            switch (sortOrder)
            {
                case "code_desc":
                    products = products.OrderByDescending(p => p.Code);
                    break;
                case "description":
                    products = products.OrderBy(p => p.Description);
                    break;
            }
            return View(products.ToList());

        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            ProductModel productmodel = db.Products.Find(id);
            if (productmodel == null)
            {
                return HttpNotFound();
            }
            return View(productmodel);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel productmodel)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(productmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productmodel);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProductModel productmodel = db.Products.Find(id);
            if (productmodel == null)
            {
                return HttpNotFound();
            }
            return View(productmodel);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel productmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productmodel);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProductModel productmodel = db.Products.Find(id);
            if (productmodel == null)
            {
                return HttpNotFound();
            }
            return View(productmodel);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductModel productmodel = db.Products.Find(id);
            db.Products.Remove(productmodel);
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
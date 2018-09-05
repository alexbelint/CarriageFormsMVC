using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_WebCargoRequestHandler.Models;

namespace MVC_WebCargoRequestHandler.Controllers
{
    public class RollingStockTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RollingStockTypes
        public ActionResult Index()
        {
            return View(db.RollingStockTypes.ToList());
        }

        // GET: RollingStockTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RollingStockType rollingStockType = db.RollingStockTypes.Find(id);
            if (rollingStockType == null)
            {
                return HttpNotFound();
            }
            return View(rollingStockType);
        }

        // GET: RollingStockTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RollingStockTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RollingStockID,RollingStockName")] RollingStockType rollingStockType)
        {
            if (ModelState.IsValid)
            {
                db.RollingStockTypes.Add(rollingStockType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rollingStockType);
        }

        // GET: RollingStockTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RollingStockType rollingStockType = db.RollingStockTypes.Find(id);
            if (rollingStockType == null)
            {
                return HttpNotFound();
            }
            return View(rollingStockType);
        }

        // POST: RollingStockTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RollingStockID,RollingStockName")] RollingStockType rollingStockType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rollingStockType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rollingStockType);
        }

        // GET: RollingStockTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RollingStockType rollingStockType = db.RollingStockTypes.Find(id);
            if (rollingStockType == null)
            {
                return HttpNotFound();
            }
            return View(rollingStockType);
        }

        // POST: RollingStockTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RollingStockType rollingStockType = db.RollingStockTypes.Find(id);
            db.RollingStockTypes.Remove(rollingStockType);
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

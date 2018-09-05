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
    public class ResidenciesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Residencies
        public ActionResult Index()
        {
            return View(db.Residencies.ToList());
        }

        // GET: Residencies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residency residency = db.Residencies.Find(id);
            if (residency == null)
            {
                return HttpNotFound();
            }
            return View(residency);
        }

        // GET: Residencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Residencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResidencyID,ResidencyName")] Residency residency)
        {
            if (ModelState.IsValid)
            {
                db.Residencies.Add(residency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(residency);
        }

        // GET: Residencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residency residency = db.Residencies.Find(id);
            if (residency == null)
            {
                return HttpNotFound();
            }
            return View(residency);
        }

        // POST: Residencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResidencyID,ResidencyName")] Residency residency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(residency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(residency);
        }

        // GET: Residencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residency residency = db.Residencies.Find(id);
            if (residency == null)
            {
                return HttpNotFound();
            }
            return View(residency);
        }

        // POST: Residencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Residency residency = db.Residencies.Find(id);
            db.Residencies.Remove(residency);
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

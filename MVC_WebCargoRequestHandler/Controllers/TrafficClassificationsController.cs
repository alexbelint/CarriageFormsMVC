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
    public class TrafficClassificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrafficClassifications
        public ActionResult Index()
        {
            return View(db.TrafficClassifications.ToList());
        }

        // GET: TrafficClassifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrafficClassification trafficClassification = db.TrafficClassifications.Find(id);
            if (trafficClassification == null)
            {
                return HttpNotFound();
            }
            return View(trafficClassification);
        }

        // GET: TrafficClassifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrafficClassifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrafficClassificationID,TrafficClassificationName")] TrafficClassification trafficClassification)
        {
            if (ModelState.IsValid)
            {
                db.TrafficClassifications.Add(trafficClassification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trafficClassification);
        }

        // GET: TrafficClassifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrafficClassification trafficClassification = db.TrafficClassifications.Find(id);
            if (trafficClassification == null)
            {
                return HttpNotFound();
            }
            return View(trafficClassification);
        }

        // POST: TrafficClassifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrafficClassificationID,TrafficClassificationName")] TrafficClassification trafficClassification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trafficClassification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trafficClassification);
        }

        // GET: TrafficClassifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrafficClassification trafficClassification = db.TrafficClassifications.Find(id);
            if (trafficClassification == null)
            {
                return HttpNotFound();
            }
            return View(trafficClassification);
        }

        // POST: TrafficClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrafficClassification trafficClassification = db.TrafficClassifications.Find(id);
            db.TrafficClassifications.Remove(trafficClassification);
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

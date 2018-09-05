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
    public class CommunicationMethodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CommunicationMethods
        public ActionResult Index()
        {
            return View(db.CommunicationMethods.ToList());
        }

        // GET: CommunicationMethods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunicationMethod communicationMethod = db.CommunicationMethods.Find(id);
            if (communicationMethod == null)
            {
                return HttpNotFound();
            }
            return View(communicationMethod);
        }

        // GET: CommunicationMethods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommunicationMethods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommunicationID,CommunicationName")] CommunicationMethod communicationMethod)
        {
            if (ModelState.IsValid)
            {
                db.CommunicationMethods.Add(communicationMethod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(communicationMethod);
        }

        // GET: CommunicationMethods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunicationMethod communicationMethod = db.CommunicationMethods.Find(id);
            if (communicationMethod == null)
            {
                return HttpNotFound();
            }
            return View(communicationMethod);
        }

        // POST: CommunicationMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommunicationID,CommunicationName")] CommunicationMethod communicationMethod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(communicationMethod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(communicationMethod);
        }

        // GET: CommunicationMethods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunicationMethod communicationMethod = db.CommunicationMethods.Find(id);
            if (communicationMethod == null)
            {
                return HttpNotFound();
            }
            return View(communicationMethod);
        }

        // POST: CommunicationMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommunicationMethod communicationMethod = db.CommunicationMethods.Find(id);
            db.CommunicationMethods.Remove(communicationMethod);
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

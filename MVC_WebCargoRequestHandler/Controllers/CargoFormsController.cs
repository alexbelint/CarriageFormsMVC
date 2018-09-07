using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVC_WebCargoRequestHandler.Models;

namespace MVC_WebCargoRequestHandler.Controllers
{
    public class CargoFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [AllowAnonymous]
        // GET: CargoForms
        public ActionResult Index()
        {
            var cargoForms = db.CargoForms.Include(c => c.CommunicationMethod).Include(c => c.Direction).Include(c => c.Residency).Include(c => c.RollingStockType).Include(c => c.TrafficClassification);
     
            return View(cargoForms.ToList());

        }

        // GET: CargoForms/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoForm cargoForm = db.CargoForms.Find(id);
            if (cargoForm == null)
            {
                return HttpNotFound();
            }
            return View(cargoForm);
        }

        // GET: CargoForms/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CommunicationID = new SelectList(db.CommunicationMethods, "CommunicationID", "CommunicationName");
            ViewBag.DirectionID = new SelectList(db.Directions, "DirectionID", "DirectionName");
            ViewBag.ResidencyID = new SelectList(db.Residencies, "ResidencyID", "ResidencyName");
            ViewBag.RollingStockID = new SelectList(db.RollingStockTypes, "RollingStockID", "RollingStockName");
            ViewBag.TrafficClassificationID = new SelectList(db.TrafficClassifications, "TrafficClassificationID", "TrafficClassificationName");
            return View();
        }

        // POST: CargoForms/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CargoFormID,ReceiptDate,CommunicationID,Customer,Departure,Destination,CargoDescription,CargoCode,RollingStockID,Cost,ResponseDate,Note,Feedback,TrafficClassificationID,DirectionID,ResidencyID")] CargoForm cargoForm)
        {
            

            if (ModelState.IsValid)
            {
                db.CargoForms.Add(cargoForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommunicationID = new SelectList(db.CommunicationMethods, "CommunicationID", "CommunicationName", cargoForm.CommunicationID);
            ViewBag.DirectionID = new SelectList(db.Directions, "DirectionID", "DirectionName", cargoForm.DirectionID);
            ViewBag.ResidencyID = new SelectList(db.Residencies, "ResidencyID", "ResidencyName", cargoForm.ResidencyID);
            ViewBag.RollingStockID = new SelectList(db.RollingStockTypes, "RollingStockID", "RollingStockName", cargoForm.RollingStockID);
            ViewBag.TrafficClassificationID = new SelectList(db.TrafficClassifications, "TrafficClassificationID", "TrafficClassificationName", cargoForm.TrafficClassificationID);
            return View(cargoForm);
        }

        // GET: CargoForms/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoForm cargoForm = db.CargoForms.Find(id);
            if (cargoForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommunicationID = new SelectList(db.CommunicationMethods, "CommunicationID", "CommunicationName", cargoForm.CommunicationID);
            ViewBag.DirectionID = new SelectList(db.Directions, "DirectionID", "DirectionName", cargoForm.DirectionID);
            ViewBag.ResidencyID = new SelectList(db.Residencies, "ResidencyID", "ResidencyName", cargoForm.ResidencyID);
            ViewBag.RollingStockID = new SelectList(db.RollingStockTypes, "RollingStockID", "RollingStockName", cargoForm.RollingStockID);
            ViewBag.TrafficClassificationID = new SelectList(db.TrafficClassifications, "TrafficClassificationID", "TrafficClassificationName", cargoForm.TrafficClassificationID);
            return View(cargoForm);
        }

        // POST: CargoForms/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CargoFormID,ReceiptDate,CommunicationID,Customer,Departure,Destination,CargoDescription,CargoCode,RollingStockID,Cost,ResponseDate,Note,Feedback,TrafficClassificationID,DirectionID,ResidencyID")] CargoForm cargoForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargoForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommunicationID = new SelectList(db.CommunicationMethods, "CommunicationID", "CommunicationName", cargoForm.CommunicationID);
            ViewBag.DirectionID = new SelectList(db.Directions, "DirectionID", "DirectionName", cargoForm.DirectionID);
            ViewBag.ResidencyID = new SelectList(db.Residencies, "ResidencyID", "ResidencyName", cargoForm.ResidencyID);
            ViewBag.RollingStockID = new SelectList(db.RollingStockTypes, "RollingStockID", "RollingStockName", cargoForm.RollingStockID);
            ViewBag.TrafficClassificationID = new SelectList(db.TrafficClassifications, "TrafficClassificationID", "TrafficClassificationName", cargoForm.TrafficClassificationID);

            //if (User.Identity.IsAuthenticated)
            //{
            //    string currentUserId = User.Identity.GetUserId();
            //    ApplicationUser applicationUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            //    string currentUser = applicationUser.UserName;
            //}
            return View(cargoForm);
        }

        // GET: CargoForms/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoForm cargoForm = db.CargoForms.Find(id);
            if (cargoForm == null)
            {
                return HttpNotFound();
            }
            return View(cargoForm);
        }

        // POST: CargoForms/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CargoForm cargoForm = db.CargoForms.Find(id);
            db.CargoForms.Remove(cargoForm);
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

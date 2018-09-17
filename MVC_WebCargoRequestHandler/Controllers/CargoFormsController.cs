﻿using System;
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
        public ActionResult Index(string sortOrder)
        {
            var cargoForms = db.CargoForms.Include(c => c.CommunicationMethod).Include(c => c.Direction).Include(c => c.Residency).Include(c => c.RollingStockType).Include(c => c.TrafficClassification);
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "CommunicationID" : "";
            ViewBag.DateSortParm = sortOrder == "ReceiptDate" ? "ResponseDate" : "ReceiptDate";
            switch (sortOrder)
            {
                case "CommunicationID":
                    cargoForms = cargoForms.OrderByDescending(s => s.CommunicationID);
                    break;
                case "ReceiptDate":
                    cargoForms = cargoForms.OrderBy(s => s.ReceiptDate);
                    break;
                case "ResponseDate":
                    cargoForms = cargoForms.OrderBy(s => s.ResponseDate);
                    break;
                default:
                    cargoForms = cargoForms.OrderBy(s => s.CommunicationID);
                    break;
            }
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
        public ActionResult Create([Bind(Include = "CargoFormID,ReceiptDate,CommunicationID,Customer,Departure,Destination,CargoDescription,CargoCode,RollingStockID,Cost,ResponseDate,Note,Feedback,TrafficClassificationID,DirectionID,ResidencyID,Author,СurrentUserId")] CargoForm cargoForm)
        {
            

            if (ModelState.IsValid)
            {
                db.CargoForms.Add(cargoForm);
                if (User.Identity.IsAuthenticated) //gather info about author
                {
                    string currentUserId = User.Identity.GetUserId();
                    ApplicationUser applicationUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                    string author = applicationUser.UserName;
                    cargoForm.Author = author;
                }

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
        public ActionResult Edit(int? id,[Bind(Include = "CargoFormID,ReceiptDate,CommunicationID,Customer,Departure,Destination,CargoDescription,CargoCode,RollingStockID,Cost,ResponseDate,Note,Feedback,TrafficClassificationID,DirectionID,ResidencyID,СurrentUserId,Author")] CargoForm cargoForm)
        {
            CargoForm cargoForms = db.CargoForms.Find(id);
            if (cargoForms == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Entry(cargoForms).State = EntityState.Modified;
                if (User.Identity.IsAuthenticated) //gather info about current user
                {
                    string currentUserId = User.Identity.GetUserId();
                    ApplicationUser applicationUser = db.Users.FirstOrDefault(x => x.Id == currentUserId); 
                    string currentUser = applicationUser.UserName;
                    cargoForms.СurrentUserId = currentUser;
                }
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

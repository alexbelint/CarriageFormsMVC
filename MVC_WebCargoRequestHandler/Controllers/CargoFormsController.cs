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
using MVC_WebCargoRequestHandler.ViewModels;
using PagedList;

namespace MVC_WebCargoRequestHandler.Controllers
{
    public class CargoFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [AllowAnonymous]
        // GET: CargoForms
        public ActionResult Index(string sortOrder,string currentFilter, string searchString, int? page)
        {
            var cargoForms = db.CargoForms.Include(c => c.CommunicationMethod)
                                          .Include(c => c.Direction)
                                          .Include(c => c.Residency)
                                          .Include(c => c.RollingStockType)
                                          .Include(c => c.TrafficClassification)
                                          .Include(c => c.Currencies);
    
            #region sorting
            ViewBag.CommunicationIDSortParm = String.IsNullOrEmpty(sortOrder) ? "CommunicationID" : "";
            ViewBag.CustomerSortParm = sortOrder == "Customer" ? "Customer_desc" : "Customer";
            ViewBag.DepartureSortParm = sortOrder == "Departure" ? "Departure_desc" : "Departure";
            ViewBag.CargoDescriptionSortParm = sortOrder == "CargoDescription" ? "CargoDescription_desc" : "CargoDescription";
            ViewBag.ReceiptDateSortParm = sortOrder == "ReceiptDate" ? "ReceiptDate_desc" : "ReceiptDate";
            ViewBag.ResponseDateSortParm = sortOrder == "ResponseDate" ? "ResponseDate_desc" : "ResponseDate";
            ViewBag.DestinationSortParm = sortOrder == "Destination" ? "Destination_desc" : "Destination";
            ViewBag.CargoCodeSortParm = sortOrder == "CargoCode" ? "CargoCode_desc" : "CargoCode";
            ViewBag.DirectionSortParm = sortOrder == "Direction" ? "Direction_desc" : "Direction";
            ViewBag.RollingStockNameSortParm = sortOrder == "RollingStockName" ? "RollingStockName_desc" : "RollingStockName";
            ViewBag.TrafficClassificationSortParm = sortOrder == "TrafficClassification" ? "TrafficClassification_desc" : "TrafficClassification";
            ViewBag.CostSortParm = sortOrder == "Cost" ? "Cost_desc" : "Cost";
            ViewBag.CurrencySortParm = sortOrder == "Currency" ? "Currency_desc" : "Currency";

            switch (sortOrder)
            {
                case "CommunicationID":
                    cargoForms = cargoForms.OrderByDescending(s => s.CommunicationID);
                    break;
                case "ReceiptDate_desc":
                    cargoForms = cargoForms.OrderByDescending(s => s.ReceiptDate);
                    break;
                case "ReceiptDate":
                    cargoForms = cargoForms.OrderBy(s => s.ReceiptDate);
                    break;
                case "ResponseDate_desc":
                    cargoForms = cargoForms.OrderByDescending(s => s.ResponseDate);
                    break;
                case "ResponseDate":
                    cargoForms = cargoForms.OrderBy(s => s.ResponseDate);
                    break;
                case "Customer":
                    cargoForms = cargoForms.OrderBy(s => s.Customer);
                    break;
                case "Customer_desc":
                    cargoForms = cargoForms.OrderByDescending(s => s.Customer);
                    break;
                case "Departure":
                    cargoForms = cargoForms.OrderBy(s => s.Departure);
                    break;
                case "Departure_desc":
                    cargoForms = cargoForms.OrderBy(s => s.Departure);
                    break;
                case "CargoDescription":
                    cargoForms = cargoForms.OrderBy(s => s.CargoDescription);
                    break;
                case "CargoDescription_desc":
                    cargoForms = cargoForms.OrderBy(s => s.CargoDescription);
                    break;
                case "Destination_desc":
                    cargoForms = cargoForms.OrderByDescending(s => s.Destination);
                    break;
                case "Destination":
                    cargoForms = cargoForms.OrderBy(s => s.Destination);
                    break;
                case "CargoCode_desc":
                    cargoForms = cargoForms.OrderByDescending(s => s.CargoCode);
                    break;
                case "CargoCode":
                    cargoForms = cargoForms.OrderBy(s => s.CargoCode);
                    break;
                case "Direction_desc":
                    cargoForms = cargoForms.OrderByDescending(s => s.Direction.DirectionName);
                    break;
                case "Direction":
                    cargoForms = cargoForms.OrderBy(s => s.Direction.DirectionName);
                    break;
                case "RollingStockName_desc":
                    cargoForms = cargoForms.OrderByDescending(s => s.RollingStockType.RollingStockName);
                    break;
                case "RollingStockName":
                    cargoForms = cargoForms.OrderBy(s => s.RollingStockType.RollingStockName);
                    break;
                case "Cost_desc":
                    cargoForms = cargoForms.OrderByDescending(s => s.Cost);
                    break;
                case "Cost":
                    cargoForms = cargoForms.OrderBy(s => s.Cost);
                    break;
                case "TrafficClassification_desc":
                    cargoForms = cargoForms.OrderByDescending(s => s.TrafficClassification.TrafficClassificationName);
                    break;
                case "TrafficClassification":
                    cargoForms = cargoForms.OrderBy(s => s.TrafficClassification.TrafficClassificationName);
                    break;
                case "Currency_desc":
                    cargoForms = cargoForms.OrderByDescending(s => s.Currencies.CurrencyName);
                    break;
                case "Currency":
                    cargoForms = cargoForms.OrderBy(s => s.Currencies.CurrencyName);
                    break;

                default:
                    cargoForms = cargoForms.OrderBy(s => s.ReceiptDate);
                    break;
            }
            #endregion

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            #region search
            if (!String.IsNullOrEmpty(searchString))
            {
                cargoForms = cargoForms.Where(s => s.Customer.Contains(searchString)
                                       || s.CommunicationMethod.CommunicationName.Contains(searchString));
            }
            #endregion
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);
           

            return View(cargoForms.ToPagedList(pageNumber, pageSize));

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
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyName");
            return View();
        }

        // POST: CargoForms/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CargoFormID,ReceiptDate,CommunicationID,Customer,Departure,Destination,CargoDescription,CargoCode,RollingStockID,Cost,ResponseDate,Note,Feedback,TrafficClassificationID,DirectionID,ResidencyID,СurrentUserId,CurrencyID")] CargoForm cargoForm)
        {
            if (ModelState.IsValid)
            {
                db.CargoForms.Add(cargoForm);
                if (User.Identity.IsAuthenticated) //gather info about user
                {
                    string currentUserId = User.Identity.GetUserId();
                    ApplicationUser applicationUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                    //string author = applicationUser.UserName;
                    //cargoForm.Author = author;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommunicationID = new SelectList(db.CommunicationMethods, "CommunicationID", "CommunicationName", cargoForm.CommunicationID);
            ViewBag.DirectionID = new SelectList(db.Directions, "DirectionID", "DirectionName", cargoForm.DirectionID);
            ViewBag.ResidencyID = new SelectList(db.Residencies, "ResidencyID", "ResidencyName", cargoForm.ResidencyID);
            ViewBag.RollingStockID = new SelectList(db.RollingStockTypes, "RollingStockID", "RollingStockName", cargoForm.RollingStockID);
            ViewBag.TrafficClassificationID = new SelectList(db.TrafficClassifications, "TrafficClassificationID", "TrafficClassificationName", cargoForm.TrafficClassificationID);
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyName", cargoForm.CurrencyID);
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
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyName", cargoForm.CurrencyID);
            return View(cargoForm);
        }

        // POST: CargoForms/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CargoFormID,ReceiptDate,CommunicationID,Customer,Departure,Destination,CargoDescription,CargoCode,RollingStockID,Cost,ResponseDate,Note,Feedback,TrafficClassificationID,DirectionID,ResidencyID,СurrentUserId,CurrencyID")] CargoForm cargoForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargoForm).State = EntityState.Modified;
                if (User.Identity.IsAuthenticated) //gather info about current user
                {
                    string currentUserId = User.Identity.GetUserId();
                    ApplicationUser applicationUser = db.Users.FirstOrDefault(x => x.Id == currentUserId); 
                    string currentUser = applicationUser.UserName;
                    cargoForm.СurrentUserId = currentUser;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommunicationID = new SelectList(db.CommunicationMethods, "CommunicationID", "CommunicationName", cargoForm.CommunicationID);
            ViewBag.DirectionID = new SelectList(db.Directions, "DirectionID", "DirectionName", cargoForm.DirectionID);
            ViewBag.ResidencyID = new SelectList(db.Residencies, "ResidencyID", "ResidencyName", cargoForm.ResidencyID);
            ViewBag.RollingStockID = new SelectList(db.RollingStockTypes, "RollingStockID", "RollingStockName", cargoForm.RollingStockID);
            ViewBag.TrafficClassificationID = new SelectList(db.TrafficClassifications, "TrafficClassificationID", "TrafficClassificationName", cargoForm.TrafficClassificationID);
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyName", cargoForm.CurrencyID);



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

        // GET: CargoForms/Clone/5
        [Authorize]
        public ActionResult Clone(int? id)
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
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyName", cargoForm.CurrencyID);
            return View(cargoForm);
        }

        // POST: CargoForms/Clone/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clone([Bind(Include = "CargoFormID,ReceiptDate,CommunicationID,Customer,Departure,Destination,CargoDescription,CargoCode,RollingStockID,Cost,ResponseDate,Note,Feedback,TrafficClassificationID,DirectionID,ResidencyID,СurrentUserId,CurrencyID")] CargoForm cargoForm)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(cargoForm).State = EntityState.Modified;
                db.CargoForms.Add(cargoForm);
                if (User.Identity.IsAuthenticated) //gather info about current user
                {
                    string currentUserId = User.Identity.GetUserId();
                    ApplicationUser applicationUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                    string currentUser = applicationUser.UserName;

                    cargoForm.СurrentUserId = currentUser;
                }
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommunicationID = new SelectList(db.CommunicationMethods, "CommunicationID", "CommunicationName", cargoForm.CommunicationID);
            ViewBag.DirectionID = new SelectList(db.Directions, "DirectionID", "DirectionName", cargoForm.DirectionID);
            ViewBag.ResidencyID = new SelectList(db.Residencies, "ResidencyID", "ResidencyName", cargoForm.ResidencyID);
            ViewBag.RollingStockID = new SelectList(db.RollingStockTypes, "RollingStockID", "RollingStockName", cargoForm.RollingStockID);
            ViewBag.TrafficClassificationID = new SelectList(db.TrafficClassifications, "TrafficClassificationID", "TrafficClassificationName", cargoForm.TrafficClassificationID);
            ViewBag.CurrencyID = new SelectList(db.Currencies, "CurrencyID", "CurrencyName", cargoForm.CurrencyID);

            return View(cargoForm);
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

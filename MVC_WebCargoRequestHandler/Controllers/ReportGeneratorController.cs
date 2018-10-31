using MVC_WebCargoRequestHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebCargoRequestHandler.Controllers
{

    public class ReportGeneratorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ReportGenerator
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetResult(CargoForm model)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var results = db.CargoForms.AsQueryable();
            if (model.Destination != null)
            {
                results = results.Where(x => x.Destination.Contains(model.Destination));
            }
            if (model.Customer != null)
            {
                results = results.Where(x => x.Customer.Contains(model.Customer));
            }
            return Json(results.ToList());
        }
    }
}
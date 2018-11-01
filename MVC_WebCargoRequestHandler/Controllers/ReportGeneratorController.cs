using MVC_WebCargoRequestHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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

        [HttpGet]
        public ActionResult GetFilteredResult(CargoForm model)
        {
            var stringPropertyNamesAndValues = model.GetType()
                .GetProperties()
                .Where(pi => pi.GetGetMethod() != null && pi.GetValue((object)model) != null && pi.PropertyType == typeof(string))
                .Select(pi => new
                {
                    Name = pi.Name,
                    Value = pi.GetGetMethod().Invoke(model, null)
                });

            db.Configuration.ProxyCreationEnabled = false;

            var results = db.CargoForms.AsQueryable();
            foreach (var obj in stringPropertyNamesAndValues)
            {
                results = results.Where($"{obj.Name}.Contains(@0)", obj.Value);
            }

            return PartialView("Results", results.ToList());
        }
    }
}
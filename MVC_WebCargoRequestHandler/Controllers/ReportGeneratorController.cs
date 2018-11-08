using MVC_WebCargoRequestHandler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace MVC_WebCargoRequestHandler.Controllers
{

    public class Filter
    {
        public string Column { get; set; }
        public string Value { get; set; }
        public bool Editing { get; set; }
    }

    public class ReportGeneratorController : Controller
    {
        private ApplicationDbContext _db;
        public ReportGeneratorController()
        {
            _db = new ApplicationDbContext();
            _db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: ReportGenerator
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetResultsPartialView(IEnumerable<Filter> filters)
        {
            var results = GetFilteredQueryable(filters);

            return PartialView("Results", results.ToList());
        }

        [HttpPost]
        public ActionResult GetFilteredResult(IEnumerable<Filter> filters)
        {
            var results = GetFilteredQueryable(filters);

            var response = results.Select($"new ({filters.Single(x => x.Editing == true).Column} as value)").Distinct();

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        private IQueryable<CargoForm> GetFilteredQueryable(IEnumerable<Filter> filters)
        {
            var results = _db.CargoForms.AsQueryable();

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    results = results.Where($"{filter.Column}.Contains(@0)", filter.Value ?? "");
                }
            }

            return results;
        }
        //[Authorize]
        [HttpPost]
        public FileContentResult ExportToExcel(IEnumerable<Filter> filters)
        {
            var results = GetFilteredQueryable(filters).ToList();
            var propertyNames = new string[] { "ReceiptDate", "Customer" };
            var header = GetCsvHeader(typeof(CargoForm), propertyNames);

            return File(new UTF8Encoding().GetBytes(header), "application/octet-stream", "CustomReport.csv");
        }

        private string GetCsvHeader(Type type, string[] propertyNames)
        {
            var displayNames = new List<string>();
            foreach(var propertyName in propertyNames)
            {
              displayNames.Add(GetDisplayNameForProperty(type, propertyName));
            }
            return String.Join(",", displayNames.ToArray());
        }

        private string GetDisplayNameForProperty(Type type, string propertyName)
        {
            string displayName = null;
            if (type.GetProperty(propertyName).GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute displayAttribute)
            {
                displayName = displayAttribute.Name;
            }

            return displayName;
        }


    }
}
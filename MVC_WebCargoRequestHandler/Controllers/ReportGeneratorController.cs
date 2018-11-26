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
using System.Data.Entity;

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
        }
        // GET: ReportGenerator
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetResultsPartialView(IEnumerable<Filter> filters, DateTime? startdate, DateTime? enddate)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            //IQueryable<CargoForm> cargoforms = _db.CargoForms;
            //if (startdate.HasValue)
            //{
            //    cargoforms = cargoforms.Where(x => x.ReceiptDate > startdate.Value);
            //}
            //if (enddate.HasValue)
            //{
            //    cargoforms = cargoforms.Where(x => x.ReceiptDate < enddate.Value);
            //}

            var results = (GetFilteredQueryable(filters)).Where(x=> startdate > x.ResponseDate && x.ResponseDate < enddate);
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
    //    [HttpPost]
    //    public JsonResult ExportToExcel(IEnumerable<Filter> filters)
    //    {
    //        var results = GetFilteredQueryable(filters).ToList();
    //        var propertyNames = new string[] { "ReceiptDate", "Customer" };
    //        var header = GetCsvHeader(typeof(CargoForm), propertyNames);
    //        var guid = Guid.NewGuid().ToString();
    //        TempData[guid] = new UTF8Encoding().GetBytes(header);
    //        return Json(new { guid });
    //    }

    //    //[Authorize]
    //    [HttpGet]
    //    public FileResult DownloadExcelFile(string guid)
    //    {
    //        byte[] byteArray = (byte[]) TempData[guid];
    //        return File(Encoding.UTF8.GetPreamble().Concat(byteArray).ToArray(), "text/csv", "CustomReport.csv");
    //    }


    //    private string GetCsvHeader(Type type, string[] propertyNames)
    //    {
    //        var displayNames = new List<string>();
    //        foreach(var propertyName in propertyNames)
    //        {
    //          displayNames.Add(GetDisplayNameForProperty(type, propertyName));
    //        }
    //        return String.Join(",", displayNames.ToArray());
    //    }

    //    private string GetDisplayNameForProperty(Type type, string propertyName)
    //    {
    //        string displayName = null;
    //        if (type.GetProperty(propertyName).GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute displayAttribute)
    //        {
    //            displayName = displayAttribute.Name;
    //        }

    //        return displayName;
    //    }
    }
}
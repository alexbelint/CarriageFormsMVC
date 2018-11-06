using MVC_WebCargoRequestHandler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
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

        //[HttpGet]
        //public ActionResult GetFilteredResult(CargoForm model)
        //{
        //    var stringPropertyNamesAndValues = model.GetType()
        //        .GetProperties()
        //        .Where(pi => pi.GetGetMethod() != null && pi.GetValue((object)model) != null && pi.PropertyType == typeof(string))
        //        .Select(pi => new
        //        {
        //            Name = pi.Name,
        //            Value = pi.GetGetMethod().Invoke(model, null)
        //        });


        //    var results = _db.CargoForms.AsQueryable();
        //    foreach (var obj in stringPropertyNamesAndValues)
        //    {
        //        results = results.Where($"{obj.Name}.Contains(@0)", obj.Value);
        //    }

        //    return PartialView("Results", results.ToList());
        //}

        [HttpPost]
        public ActionResult GetFilteredResult(List<Filter> filters)
        {
            var results = _db.CargoForms.AsQueryable();

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    results = results.Where($"{filter.Column}.Contains(@0)", filter.Value ?? "");
                }
            }
            
            var response = results.Select($"new ({filters.Single(x => x.Editing == true).Column} as value)").Distinct();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        //[Authorize]
        //[HttpPost]
        //public ActionResult ExportToExcel(CargoForm cargoForm)
        //{
        //    var cargoFormsSearchedList = new DataTable("customTestTable");

        //    var grid = new GridView();
        //    grid.DataSource = cargoFormsSearchedList;
        //    grid.DataBind();

        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=CargoForms Report.xls");
        //    Response.ContentType = "application/ms-excel";

        //    Response.Charset = "";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);

        //    grid.RenderControl(htw);

        //    Response.Output.Write(sw.ToString());
        //    Response.Flush();
        //    Response.End();

        //    return View(cargoForm);
        //}
    }
}
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

        [HttpPost]
        public ActionResult GetResultsPartialView(List<Filter> filters)
        {
            var results = GetFilteredQueryable(filters);

            return PartialView("Results", results.ToList());
        }

        [HttpPost]
        public ActionResult GetFilteredResult(List<Filter> filters)
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
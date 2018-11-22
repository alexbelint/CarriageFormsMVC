using MVC_WebCargoRequestHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_WebCargoRequestHandler.ViewModels
{
    public class SearchViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ICollection<CargoForm> CargoForms { get; set; }
    }
}
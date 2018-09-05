using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebCargoRequestHandler.Models
{
    public class RollingStockType
    {
        [Key]
        public int RollingStockID { get; set; }

        [Display(Name = "Тип ПС")]
        public string RollingStockName { get; set; }

        public virtual ICollection<CargoForm> CargoForms { get; set; }
    }
}
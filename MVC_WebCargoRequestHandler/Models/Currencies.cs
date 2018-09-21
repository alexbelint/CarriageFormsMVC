using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebCargoRequestHandler.Models
{
    public class Currencies
    {
        [Key]
        public int CurrencyID { get; set; }

        [Display(Name = "Валюта")]
        public string CurrencyName { get; set; }

        public virtual ICollection<CargoForm> CargoForms { get; set; }
    }
}
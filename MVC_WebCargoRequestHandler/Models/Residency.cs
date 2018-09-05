using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebCargoRequestHandler.Models
{
    public class Residency
    {
        [Key]
        public int ResidencyID { get; set; }

        [Display(Name = "Резиденство")]
        public string ResidencyName { get; set; }

        public virtual ICollection<CargoForm> CargoForms { get; set; }
    }
}
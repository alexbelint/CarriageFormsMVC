using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebCargoRequestHandler.Models
{
    public class Direction
    {
        [Key]
        public int DirectionID { get; set; }

        [Display(Name = "Направление")]
        public string DirectionName { get; set; }

        public virtual ICollection<CargoForm> CargoForms { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebCargoRequestHandler.Models
{
    public class TrafficClassification
    {
        [Key]
        public int TrafficClassificationID { get; set; }

        [Display(Name = "Вид сообщ.")]
        public string TrafficClassificationName { get; set; }

        public virtual ICollection<CargoForm> CargoForms { get; set; }
    }
}
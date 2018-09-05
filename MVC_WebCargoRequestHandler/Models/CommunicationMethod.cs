using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebCargoRequestHandler.Models
{
    public class CommunicationMethod
    {
        [Key]
        [Display(Name = "Вид связи")]
        public int CommunicationID { get; set; }

        [Display(Name = "Вид связи")]
        public string CommunicationName { get; set; }

        public virtual ICollection<CargoForm> CargoForms { get; set; }
    }
}
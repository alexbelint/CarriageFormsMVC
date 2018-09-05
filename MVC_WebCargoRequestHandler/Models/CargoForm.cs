using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebCargoRequestHandler.Models
{
    public class CargoForm
    {
        public int CargoFormID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, HtmlEncode = false)]
        [Display(Name = "Дата пост. заявки")]
        public string ReceiptDate { get; set; } //Дата получения

        public int CommunicationID { get; set; }
        public virtual CommunicationMethod CommunicationMethod { get; set; }//Способ связи: звонок/email/skype

        [Display(Name = "Заказчик")]
        public string Customer { get; set; } //Заказчик: физ.лицо/название фирмы

        [Display(Name = "Станция отправления")]
        public string Departure { get; set; } //Место отправления

        [Display(Name = "Станция назначения")]
        public string Destination { get; set; } //Место назначения

        [Display(Name = "Описание груза")]
        public string CargoDescription { get; set; } //Описание груза: генеральный/авто/уголь и т.п.

        [Display(Name = "ГНГ/ЕТСНГ")]
        public string CargoCode { get; set; } //ГНГ/ЕТСНГ

        public int RollingStockID { get; set; }
        public virtual RollingStockType RollingStockType { get; set; }//Тип подвижного состава: (цистерна СПС/ 40фут)


        [Display(Name = "Стоимость")]
        public string Cost { get; set; } //Стоимость


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, HtmlEncode = false)]
        [Display(Name = "Дата ответа")]
        public string ResponseDate { get; set; } //Дата ответа

        [Display(Name = "Примечание")]
        public string Note { get; set; } //Примечание

        [Display(Name = "Обратная связь")]
        public string Feedback { get; set; } //Обратная связь

        public int TrafficClassificationID { get; set; }
        public virtual TrafficClassification TrafficClassification { get; set; }//Вид сообщения

        public int DirectionID { get; set; }
        public virtual Direction Direction { get; set; }//Вид сообщения

        public int ResidencyID { get; set; }
        public virtual Residency Residency { get; set; }//Рез/нерез
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hostal_El_Eden.Models
{
    public class ReservationSearch 
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ResCheckinDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ResCheckoutDate { get; set; }

        public int People { get; set; }
        public static List<SelectListItem> Choices
        {
            get
            {
                return new List<SelectListItem>() { new SelectListItem{ Text = "1", Value = "1" }, new SelectListItem{ Text = "2", Value = "2" } , new SelectListItem{ Text = "3", Value = "3" } , new SelectListItem{ Text = "4", Value = "4" } };
            }
            set
            {
                Choices = value;
            }
        }
    }

    
}

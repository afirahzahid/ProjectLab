using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project2.Models
{
    public class DateofBirth
    {
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime S_DOB { get; set; }
    }
}
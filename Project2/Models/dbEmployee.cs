//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project2.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;
    using System.Linq;

    public partial class dbEmployee
    {
        public class UniqueECNICAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                dbHostelManagementEntities db = new dbHostelManagementEntities();
                var userWithTheSameECNIC = db.dbEmployees.SingleOrDefault(
                    u => u.EmpCNIC == (Int64)value);
                return userWithTheSameECNIC == null;
            }

        }
        [Required]
        [Display(Name = "CNIC")]
        [UniqueECNIC(ErrorMessage = "This CNIC is already registered")]
        [RegularExpression(@"^([0-9]{13})*$",
            ErrorMessage = "CNIC length must be 13 and without dashes")]
        public long EmpCNIC { get; set; }
        [Required]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z ]*$",
            ErrorMessage = "Invalid characters in Name")]
        public string EmpName { get; set; }
        [Required]
        [Display(Name = "Father Name")]
        [RegularExpression(@"^[a-zA-Z ]*$",
            ErrorMessage = "Invalid characters in Father Name")]
        public string EmpFatherName { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{10})*$",
            ErrorMessage = "Invalid Phone No")]
        [Display(Name = "PhoneNo")]
        public long EmpPhoneNo { get; set; }
      
        [Required]
        [Range(typeof(DateTime), "1/1/1970", "1/1/2000",
                ErrorMessage = "Value for {0} must be between {1:d} and {2:d}")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat( DataFormatString = "{MM/dd/yyyy}")]
        public System.DateTime EmpDOB { get; set; }
        [Required]
        [Display(Name = "Designation")]
        public string EmpDesignation { get; set; }
        [Display(Name = "Active")]
        public bool EmpIsActive { get; set; }

        public class UniqueEmpEmailAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                dbHostelManagementEntities db = new dbHostelManagementEntities();
                var userWithTheSameCNIC = db.dbEmployees.SingleOrDefault(
                    u => u.EmpEmail == (string)value);
                return userWithTheSameCNIC == null;
            }

        }
        [Required]
        [Display(Name = "Email")]
        [UniqueEmpEmail(ErrorMessage = "This Email Id is already registered")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmpEmail { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6,
            ErrorMessage = "passwords must be a minimum of 6 characters")]
        public string EmpPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("EmpPassword")]
        [Required(ErrorMessage = "Confirm Password is Required")]
        public string EmpConfirmPassword { get; set; }
        
        public static IEnumerable<SelectListItem> GetHostelList()
        {
            IList<SelectListItem> i = new List<SelectListItem>
            {
                new SelectListItem{Text = "Khadija Hall", Value = "Khadija Hall"},
                new SelectListItem{Text  = "C Hall", Value = "C Hall"},
                new SelectListItem{Text = "Ayesha Hall", Value = "Ayesha Hall"},
                new SelectListItem{Text = "Zohra Hall", Value = "Zohra Hall"},
            };
            return i;
        }
        public string EmpHostelName { get; set; }
    }
}
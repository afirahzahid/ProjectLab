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
    
    public partial class dbFoodItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dbFoodItem()
        {
            this.dbMessAttendances = new HashSet<dbMessAttendance>();
        }
    
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int FoodPrice { get; set; }
        public string FoodDay { get; set; }
        public string FoodType { get; set; }
        public int Food_MenuId { get; set; }
    
        public virtual dbMenu dbMenu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dbMessAttendance> dbMessAttendances { get; set; }
    }
}

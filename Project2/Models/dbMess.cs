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
    
    public partial class dbMess
    {
        public int MessId { get; set; }
        public long Mess_S_CNIC { get; set; }
        public System.DateTime MessCurrentDate { get; set; }
        public Nullable<int> MessLunch { get; set; }
        public Nullable<int> MessDinner { get; set; }
    
        public virtual dbStudent dbStudent { get; set; }
    }
}

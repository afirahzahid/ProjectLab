﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbHostelManagementEntities : DbContext
    {
        public dbHostelManagementEntities()
            : base("name=dbHostelManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<dbAllotment> dbAllotments { get; set; }
        public virtual DbSet<dbEmployee> dbEmployees { get; set; }
        public virtual DbSet<dbFoodItem> dbFoodItems { get; set; }
        public virtual DbSet<dbHostel> dbHostels { get; set; }
        public virtual DbSet<dbLogin> dbLogins { get; set; }
        public virtual DbSet<dbMenu> dbMenus { get; set; }
        public virtual DbSet<dbMess> dbMesses { get; set; }
        public virtual DbSet<dbMessAttendance> dbMessAttendances { get; set; }
        public virtual DbSet<dbRoom> dbRooms { get; set; }
        public virtual DbSet<dbStudent> dbStudents { get; set; }
    }
}

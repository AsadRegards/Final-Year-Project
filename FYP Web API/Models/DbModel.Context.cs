﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FYP_Web_API.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class problemupdatedbEntities : DbContext
    {
        public problemupdatedbEntities()
            : base("name=problemupdatedbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ad_table> ad_table { get; set; }
        public virtual DbSet<admin_table> admin_table { get; set; }
        public virtual DbSet<AssignedByAdmin> AssignedByAdmin { get; set; }
        public virtual DbSet<FCM_TOKEN> FCM_TOKEN { get; set; }
        public virtual DbSet<funds_table> funds_table { get; set; }
        public virtual DbSet<issue_images_table> issue_images_table { get; set; }
        public virtual DbSet<issue_table> issue_table { get; set; }
        public virtual DbSet<nearby_user_table> nearby_user_table { get; set; }
        public virtual DbSet<NotificationTable> NotificationTable { get; set; }
        public virtual DbSet<report_table> report_table { get; set; }
        public virtual DbSet<user_table> user_table { get; set; }
        public virtual DbSet<Volunteer_table> Volunteer_table { get; set; }
        public virtual DbSet<PaymentMethodInfo> PaymentMethodInfo { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clinic_Automation.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ClinicAutomationEntities : DbContext
    {
        public ClinicAutomationEntities()
            : base("name=ClinicAutomationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Chemist> Chemists { get; set; }
        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<DrugRequest> DrugRequests { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Physician> Physicians { get; set; }
        public virtual DbSet<PhysicianAdvice> PhysicianAdvices { get; set; }
        public virtual DbSet<PhysicianPrescription> PhysicianPrescriptions { get; set; }
        public virtual DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        public virtual DbSet<PurchaseProductLine> PurchaseProductLines { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}

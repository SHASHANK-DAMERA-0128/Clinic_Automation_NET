//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class PurchaseProductLine
    {
        public int PurchaseOrderID { get; set; }
        public int DrugId { get; set; }
        public int SlNo { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
    
        public virtual Drug Drug { get; set; }
        public virtual PurchaseOrderHeader PurchaseOrderHeader { get; set; }
    }
}

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
    
    public partial class Physician
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Physician()
        {
            this.Users = new HashSet<User>();
        }
    
        public int Physician_ID { get; set; }
        public string Physician_Name { get; set; }
        public System.DateTime DOB { get; set; }
        public string Specialization { get; set; }
        public string Gender { get; set; }
        public string Contact_Number { get; set; }
        public string Email_ID { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinic_Automation.Models
{
    public class supplierExtend
    {
        [Display(Name = "Supplier Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Supplier Name")]
        public string SupplierName { get; set; }
        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Address")]
        public string SupplierAddress { get; set; }
        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Phone Number")]
        [MaxLength(10, ErrorMessage = "Please Enter 10 Digit Phone Number")]
        [MinLength(10, ErrorMessage = "Please Enter 10 Digit Phone Number")]
        public string SupplierNumber { get; set; }
        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Email Address")]
        public string SupplierEmailID { get; set; }
    }
    [MetadataType(typeof(supplierExtend))]
    public partial class Supplier
    {
    }
}
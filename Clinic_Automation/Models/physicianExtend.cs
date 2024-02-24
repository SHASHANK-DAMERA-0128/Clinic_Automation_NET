using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinic_Automation.Models
{
    public class physicianExtend
    {
        [Display(Name = "Physician Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Physician Name")]
        public string PhysicianName { get; set; }
        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Address")]
        public string PhysicianAddress { get; set; }
        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Phone Number")]
        [MaxLength(10, ErrorMessage = "Please Enter 10 Digit Phone Number")]
        [MinLength(10, ErrorMessage = "Please Enter 10 Digit Phone Number")]
        public string PhysicianNumber { get; set; }
        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string PhysicianEmailID { get; set; }
        [Display(Name = "Specialization")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid specialization")]
        public string Specialization { get; set; }
        [Display(Name = "Summary")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Summary")]
        public string PhysicianSummary { get; set; }
    }

    [MetadataType(typeof(physicianExtend))]
    public partial class Physician
    {
    }
}
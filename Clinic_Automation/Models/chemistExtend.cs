using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinic_Automation.Models
{
    public class chemistExtend
    {
        [Display(Name = "Chemist Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Chemist Name")]
        public string ChemistName { get; set; }
        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Address")]
        public string ChemistAddress { get; set; }
        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Phone Number")]
        [MaxLength(10, ErrorMessage = "Please Enter 10 Digit Phone Number")]
        [MinLength(10, ErrorMessage = "Please Enter 10 Digit Phone Number")]
        public string ChemistNumber { get; set; }
        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string ChemistEmailID { get; set; }
        [Display(Name = "Summary")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Summary")]
        public string ChemistSummary { get; set; }
    }

    [MetadataType(typeof(chemistExtend))]

    public partial class Chemist
    {
    }
}
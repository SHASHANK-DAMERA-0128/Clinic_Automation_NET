using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Clinic_Automation.Models
{
   
    public class Signinmetadata
    {
        [Display(Name ="Patient Name")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please Enter a valid Patient Name")]
        public string PatientName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a valid Date of Birth")]
        [DataType(DataType.Date)]
        public System.DateTime DOB { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a valid Mobile Number")]
        [MaxLength(10, ErrorMessage = "Please Enter 10 Digit Phone Number")]
        [MinLength(10, ErrorMessage = "Please Enter 10 Digit Phone Number")]
        public string PatientNumber { get; set; }

        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a valid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string PatientEmail { get; set; }

        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a valid Address")]
        public string PatientAddress { get; set; }

        [Display(Name = "Gender")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please type either 'Male' or 'Female'")]
        [MinLength(4, ErrorMessage = "Please type either 'Male' or 'Female'")]
        [MaxLength(6, ErrorMessage = "Please type either 'Male' or 'Female'")]
        public string Gender { get; set; }
        [Display(Name = "Patient Summary")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a valid Summary")]
        public string PatientSummary { get; set; }
    }
    [MetadataType(typeof(Signinmetadata))]
    public partial class Patient
    {

    }
}
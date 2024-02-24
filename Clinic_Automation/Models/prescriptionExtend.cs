using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinic_Automation.Models
{
    public class prescriptionExtend
    {
        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Description")]
        public string Description { get; set; }
    }
    [MetadataType(typeof(prescriptionExtend))]



    public partial class PhysicianPrescription
    {
    }
}
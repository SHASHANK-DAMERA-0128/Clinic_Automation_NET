using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinic_Automation.Models
{
    public class adviseExtend
    {
        [Display(Name = "Advice")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a Advice")]
        public string Advise { get; set; }
    }
    [MetadataType(typeof(adviseExtend))]

    public partial class PhysicianAdvice
    {
    }
}
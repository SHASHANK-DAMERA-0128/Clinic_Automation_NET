using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinic_Automation.Models
{
    [MetadataType(typeof(userMetadata))]
    public partial class userExtend
    {
        public string confirmPassword {  get; set; }    
    }

    public class userMetadata
    {
        [Display(Name = "User Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid User Name")]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the Confirm Password")]
        [Compare("Password",ErrorMessage ="Confirm Password and Password do not match")]
        public string confirmPassword { get; set; }

    }
}
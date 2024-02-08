using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinic_Automation.Models
{
    public class LoginExtend
    {
        [Display(Name ="User Name")]
        [Required]
        public string User_Name { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
    }

    [MetadataType(typeof(LoginExtend))]
    public partial class userTable
    {

    }
}
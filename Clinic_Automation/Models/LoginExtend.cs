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
        //[RegularExpression("/^(?=.*\\d)(?=.*[a-zA-Z]{4}).{6,15}$",
        //    ErrorMessage = "User Name should not contain special Character, be longer than 2 charaters and be less than 15 Character.")]
        [Required]
        public string User_Name { get; set; }


        [Display(Name = "Password")]
        [Required]
        //[RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+\\-=[\\]{};':\"|,.<>?]).{7,15}$",
        //    ErrorMessage = "Password must contain at least one Captial Letter, at least one number,at least one special Character, be longer than 6 charaters and be less than 15 Character ..")]
        public string Password { get; set; }
    }

    [MetadataType(typeof(LoginExtend))]
    public partial class userTable
    {

    }
}
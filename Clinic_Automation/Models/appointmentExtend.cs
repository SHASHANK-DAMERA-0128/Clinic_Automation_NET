using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Clinic_Automation.Models.customValidation;


namespace Clinic_Automation.Models
{
    public class appointmentExtend
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a physician.")]
        public int PhysicianID { get; set; }
        [Display(Name = "Appointment Date & Time")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a valid Date and Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-mm-yyyy HH:mm:ss}")]
        [appdatetimevalidation(ErrorMessage = "You have chosen a date in the past. Please select a future date to proceed.")]
        public System.DateTime AppointmentDateTime { get; set; }
        [Display(Name = "Criticality")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a valid Criticality")]
        public string Criticality { get; set; }
        [Display(Name = "Reason")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a valid Reason")]
        public string Reason { get; set; }
        [Display(Name = "Note")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a valid Note")]
        public string Note { get; set; }

    }
    [MetadataType(typeof(appointmentExtend))]
    public partial class Appointment
    {
    }
}
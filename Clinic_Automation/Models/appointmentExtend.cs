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


        //[Display(Name = "Appointment Date & Time")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter the valid Date and Time")]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        //[appdatetimevalidation(ErrorMessage = "Please Enter the valid Date and Time")]
        //public System.DateTime AppointmentDateTime { get; set; }
        [Display(Name = "Appointment Date & Time")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a valid Date and Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm tt}")]
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
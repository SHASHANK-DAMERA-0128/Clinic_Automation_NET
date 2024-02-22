using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic_Automation.Models
{
    public class AdvicePrescriptionModel
    {
        public Models.PhysicianAdvice Advice { get; set; }
        public Models.PhysicianPrescription Prescription { get; set; }
        public Models.PhysicianPrescription[] Prescriptions { get; set; }
    }

    public class PrescriptionDrugModel
    {
        public int DrugID { get; set; }
        public string DrugName { get; set; }
    }
}
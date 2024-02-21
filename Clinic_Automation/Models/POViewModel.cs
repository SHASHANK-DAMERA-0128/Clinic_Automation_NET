using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic_Automation.Models
{
    public class POViewModel
    {
        public Models.PurchaseOrderHeader POHeader { get; set; }
        public Models.PurchaseProductLine POProductLine { get; set; }
        public Models.PurchaseProductLine[] POProductLines { get; set; }
    }

    public class DrugModel
    {
        public int DrugID { get; set; }
        public string DrugName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Automation.Controllers
{
    public class ChemistController : Controller
    {
        // GET: Chemist
        [Authorize(Roles = "CHEMIST")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewMedicine()
        {
            return View();
        }
    }
}
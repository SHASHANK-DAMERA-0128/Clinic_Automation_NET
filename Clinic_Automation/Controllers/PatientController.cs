using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Automation.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        [Authorize(Roles = "PATIENT")]

        public ActionResult Index()
        {
            return View();
        }
    }
}
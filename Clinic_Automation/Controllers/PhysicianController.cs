using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Automation.Controllers
{
    public class PhysicianController : Controller
    {
        // GET: Physician
        [Authorize(Roles = "PHYSICIAN")]
        public ActionResult Index()
        {
            
            return View();
        }
    }
}
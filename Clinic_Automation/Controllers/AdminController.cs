using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Automation.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles ="ADMIN")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
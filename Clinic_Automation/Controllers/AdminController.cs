using Clinic_Automation.Models;
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

        Models.ClinicAutomationEntities _db = new Models.ClinicAutomationEntities();
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddNewPhysician()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewPhysician(Models.Physician physician)
        {
            if (ModelState.IsValid)
            {
                _db.Physicians.Add(physician);
                _db.SaveChanges();

                User usr = new User();
                usr.Reference_ID = physician.Physician_ID;
                usr.User_Name = physician.Physician_Name;
                usr.Password = physician.Physician_Name + "@" + "A" + "1";
                usr.Role = "PHYSICIAN";

                _db.Users.Add(usr);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }


            return View();
        }


    }
}
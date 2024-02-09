using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clinic_Automation.Models;

namespace Clinic_Automation.Controllers
{
    public class AdminController : Controller
    {
        private ClinicAutomationEntities _db = new ClinicAutomationEntities();
        [Authorize(Roles = "ADMIN")]
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayAdminPhysician()
        {
            return View(_db.Physicians.ToList());
        }

        public ActionResult CreateAdminPhysician()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdminPhysician([Bind(Include = "PhysicianID,PhysicianName,PhysicianAddress,PhysicianNumber,PhysicianEmailID,Specialization,PhysicianSummary")] Physician physician)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                _db.Physicians.Add(physician);
                _db.SaveChanges();
                User usr = new User();
                usr.ReferenceToID = physician.PhysicianID;
                usr.UserName = physician.PhysicianName + rnd.Next(1000, 10000);
                usr.Password = physician.PhysicianName + rnd.Next(1000, 10000) + "@" + "A" + "1";
                usr.Role = "PHYSICIAN";

                _db.Users.Add(usr);
                _db.SaveChanges();
                return RedirectToAction("DisplayAdminPhysician");
            }

            return View(physician);
        }


        public ActionResult EditAdminPhysician(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Physician physician = _db.Physicians.Find(id);
            if (physician == null)
            {
                return HttpNotFound();
            }
            return View(physician);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdminPhysician([Bind(Include = "PhysicianID,PhysicianName,PhysicianAddress,PhysicianNumber,PhysicianEmailID,Specialization,PhysicianSummary")] Physician physician)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(physician).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("DisplayAdminPhysician");
            }
            return View(physician);
        }


        public ActionResult DeleteAdminPhysician(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Physician physician = _db.Physicians.Find(id);
            if (physician == null)
            {
                return HttpNotFound();
            }
            return View(physician);
        }

        [HttpPost, ActionName("DeleteAdminPhysician")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedAdminPhysician(int id)
        {
            Physician physician = _db.Physicians.Find(id);
            _db.Physicians.Remove(physician);
            _db.SaveChanges();
            return RedirectToAction("DisplayAdminPhysician");
        }
























        //--------------------------PATIENT---------


        public ActionResult DisplayAdminPatient()
        {
            return View(_db.Patients.ToList());
        }

        public ActionResult CreateAdminPatient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdminPatient([Bind(Include = "PatientID,PatientName,DOB,PatientNumber,PatientEmail,PatientAddress,Gender,PatientSummary")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                _db.Patients.Add(patient);
                _db.SaveChanges();
                User usr = new User();
                usr.ReferenceToID = patient.PatientID;
                usr.UserName = patient.PatientName + rnd.Next(1000, 10000);
                usr.Password = patient.PatientName + rnd.Next(1000, 10000) + "@" + "A" + "1";
                usr.Role = "PATIENT";

                _db.Users.Add(usr);
                _db.SaveChanges();
                return RedirectToAction("DisplayAdminPatient");
            }

            return View(patient);
        }


        public ActionResult EditAdminPatient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = _db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdminPatient([Bind(Include = "PatientID,PatientName,DOB,PatientNumber,PatientEmail,PatientAddress,Gender,PatientSummary")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(patient).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("DisplayAdminPatient");
            }
            return View(patient);
        }


        public ActionResult DeleteAdminPatient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = _db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [HttpPost, ActionName("DeleteAdminPatient")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedAdminPatient(int id)
        {
            Patient patient = _db.Patients.Find(id);
            _db.Patients.Remove(patient);
            _db.SaveChanges();
            return RedirectToAction("DisplayAdminPatient");
        }






        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

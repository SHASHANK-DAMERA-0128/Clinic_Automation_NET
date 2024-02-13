using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clinic_Automation.Models;
using Microsoft.Ajax.Utilities;

namespace Clinic_Automation.Controllers
{
    public class PatientController : Controller
    {
        private ClinicAutomationEntities1 db = new ClinicAutomationEntities1();
        [Authorize(Roles = "PATIENT")]
        // GET: Patient
        
        public ActionResult Index()
        {
            var curr_usr = Session["CurrentUser"] as CurrentUser;

            if (curr_usr.ReferenceToID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Appointment> appointments = db.Appointments.Where(a => a.PatientID == curr_usr.ReferenceToID).ToList();
            if (appointments.Count == 0)
            {
                return HttpNotFound();
            }
            ViewBag.CurrentUserName = curr_usr.UserName;
            return View(appointments);


        }

        public ActionResult CreateAppointment()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAppointment([Bind(Include = "AppointmentID,PhysicianID,AppointmentDateTime,Criticality,Reason,Note")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var curr_usr = Session["CurrentUser"] as CurrentUser;
                appointment.PatientID = (int)curr_usr.ReferenceToID;
                appointment.ScheduleStatus = "PENDING";
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }
        public ActionResult ViewProfile()
        {
            var curr_usr = Session["CurrentUser"] as CurrentUser;

            if (curr_usr.ReferenceToID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.FirstOrDefault(a => a.PatientID == curr_usr.ReferenceToID);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }
        public ActionResult EditProfile()
        {
            var curr_usr = Session["CurrentUser"] as CurrentUser;
            if (curr_usr.ReferenceToID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.FirstOrDefault(a => a.PatientID == curr_usr.ReferenceToID);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "PatientID,PatientName,DOB,PatientNumber,PatientEmail,PatientAddress,Gender,PatientSummary")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewProfile");
            }
            return View(patient);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using Clinic_Automation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Automation.Controllers
{
    [Authorize(Roles = "PHYSICIAN")]
    public class PhysicianController : Controller
    {
        private ClinicAutomationEntities db = new ClinicAutomationEntities();

        public ActionResult Index()
        {
            try
            {
                var curr_usr = Session["CurrentUser"] as CurrentUser;

                if (curr_usr?.ReferenceToID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var appointmentid = db.Appointments.Where(a => a.PhysicianID == curr_usr.ReferenceToID && a.ScheduleStatus == "APPROVED").Select(a => a.AppointmentID).ToList();
                List<Schedule> schedules = db.Schedules
                                .Where(a => appointmentid.Contains(a.AppointmentID) && a.ScheduleStatus == "APPROVED")
                                .ToList();
                if (schedules.Count == 0)
                {
                    return View(schedules);
                }
                ViewBag.CurrentUserName = curr_usr.UserName;
                return View(schedules);

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        public ActionResult DisplayPhysicianPrescription(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<PhysicianPrescription> physicianPrescription = db.PhysicianPrescriptions.Where(a => a.ScheduleID == id).ToList();
            var AdviceData = db.PhysicianAdvices.Where(a => a.ScheduleID == id).FirstOrDefault();

            if (AdviceData == null)
                return RedirectToAction("CreatePhysicianPrescription", new { id = id });

            ViewBag.ScheduleID = id;

            return View(physicianPrescription);
        }


        public ActionResult CreatePhysicianPrescription(int? id)
        {
            var lst = db.Drugs.ToList().Select(d => new DrugModel { DrugName = d.Title, DrugID = d.DrugID }).ToList();

            ViewBag.DrugID = new SelectList(lst, "DrugID", "DrugName");
            ViewBag.DrugDataList = lst;
            ViewBag.ScheduleID = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePhysicianPrescription(Models.AdvicePrescriptionModel adm)
        {
            db.PhysicianAdvices.Add(adm.Advice);
            db.SaveChanges();

            // Get the PhysicianAdviceID after saving
            int physicianAdviceId = adm.Advice.PhysicianAdviceID;

            // Update Prescription objects with PhysicianAdviceID and ScheduleID
            foreach (var prescription in adm.Prescriptions)
            {
                prescription.PhysicianAdviceID = physicianAdviceId; // Use the obtained PhysicianAdviceID
                prescription.ScheduleID = adm.Advice.ScheduleID;
                db.PhysicianPrescriptions.Add(prescription);
            }

            // Save changes to the database
            db.SaveChanges();

            return RedirectToAction("Index", "Physician");
        }


        public ActionResult EditPhysicianPrescription(int? id)
        {
            ViewBag.ScheduleID = new SelectList(db.Schedules, "ScheduleID", "ScheduleID");
            ViewBag.DrugID = new SelectList(db.Drugs, "DrugID", "DrugID");
            ViewBag.PhysicianAdviceID = new SelectList(db.PhysicianAdvices, "PhysicianAdviceID", "PhysicianAdviceID");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicianPrescription physicianPrescription = db.PhysicianPrescriptions.Find(id);
            if (physicianPrescription == null)
            {
                return HttpNotFound();
            }
            return View(physicianPrescription);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPhysicianPrescription([Bind(Include = "PhysicianPrescriptionID,ScheduleID,DrugID,PhysicianAdviceID,Description")] PhysicianPrescription physicianPrescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(physicianPrescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DisplayPhysicianPrescription", new { id = physicianPrescription.ScheduleID });
            }
            return View(physicianPrescription);
        }

    }

}
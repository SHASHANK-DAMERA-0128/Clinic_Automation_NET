using System;
using Clinic_Automation.Models;
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
        // GET: Physician
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
                                .OrderByDescending(a => a.ScheduleDate)
                                .ToList();
                if (schedules.Count == 0)
                {
                    return View(schedules);
                }
                ViewBag.CurrentUserName = curr_usr.UserName;
                return View(schedules);

            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public ActionResult DisplayPhysicianPrescription(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            List<PhysicianPrescription> physicianPrescription = db.PhysicianPrescriptions.Where(a => a.ScheduleID == id).ToList();
            var AdviceData = db.PhysicianAdvices.Where(a => a.ScheduleID == id).FirstOrDefault();
            ViewBag.ScheduleID = id;

            if (AdviceData == null)
                return RedirectToAction("CreatePhysicianPrescription", new { id = id });

            ViewBag.AdviceData = AdviceData;

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




        public ActionResult EditPhysicianAdvice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicianAdvice physicianadvice = db.PhysicianAdvices.Find(id);
            if (physicianadvice == null)
            {
                return HttpNotFound();
            }
            return View(physicianadvice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPhysicianAdvice([Bind(Include = "PhysicianAdviceID,ScheduleID,Advise")] PhysicianAdvice physicianadvice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(physicianadvice).State = EntityState.Modified;
                physicianadvice.Schedule = db.Schedules.Find(physicianadvice.ScheduleID);
                db.SaveChanges();
                return RedirectToAction("DisplayPhysicianPrescription", new { id = physicianadvice.ScheduleID });
            }
            return RedirectToAction("DisplayPhysicianPrescription");
        }


        public ActionResult PhysiciandrugRequest()
        {
            var drugRequests = db.DrugRequests.Include(d => d.Physician);
            return View(drugRequests.ToList());
        }
        public ActionResult CreatePhysicianDrugRequest()
        {
            ViewBag.PhysicianID = new SelectList(db.Physicians, "PhysicianID", "PhysicianName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePhysicianDrugRequest([Bind(Include = "DrugRequestID,PhysicianID,DrugInfoText,RequestStatus")] DrugRequest drugRequest)
        {
            if (ModelState.IsValid)
            {
                drugRequest.RequestStatus = "Not";
                drugRequest.RequestedDate = DateTime.Now;
                db.DrugRequests.Add(drugRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PhysicianID = new SelectList(db.Physicians, "PhysicianID", "PhysicianName", drugRequest.PhysicianID);
            return View(drugRequest);
        }

    }



}
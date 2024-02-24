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
        private ClinicAutomationEntities db = new ClinicAutomationEntities();
        [Authorize(Roles = "PATIENT")]
        // GET: Patient

        public ActionResult Index(int? page, string statusFilter = null, string physicianFilter = null, int pageSize = 5)
        {
            var curr_usr = Session["CurrentUser"] as CurrentUser;

            if (curr_usr.ReferenceToID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var query = db.Appointments.Where(a => a.PatientID == curr_usr.ReferenceToID);

            var patientPhysicianIds = query.Select(a => a.PhysicianID).Distinct().ToList();
            var physicians = db.Physicians.Where(p => patientPhysicianIds.Contains(p.PhysicianID)).ToList();
            ViewBag.Physicians = physicians;

            // Apply search filter by physician name if provided
            if (!string.IsNullOrEmpty(physicianFilter))
            {
                query = query.Where(a => a.Physician.PhysicianName.Contains(physicianFilter));
            }

            // Apply status filter if provided
            if (!string.IsNullOrEmpty(statusFilter))
            {
                query = query.Where(a => a.ScheduleStatus == statusFilter);
            }

            int pageNumber = (page ?? 1);
            int totalItems = query.Count();

            var appointments = query.OrderByDescending(a => a.AppointmentDateTime)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.StatusFilter = statusFilter;
            ViewBag.PhysicianFilter = physicianFilter;

            return View(appointments);
        }

        public ActionResult CreateAppointment()
        {

            var physicianList = db.Physicians.Select(p => new
            {
                p.PhysicianID,
                FullName = p.PhysicianName + " (" + p.Specialization + ")"
            }).ToList();
            ViewBag.PhysicianID = new SelectList(physicianList, "PhysicianID", "Fullname");
            SelectList CriticalityList = new SelectList(new[]
                {
                new SelectListItem { Text = "Normal", Value = "Normal" },
                new SelectListItem { Text = "Critical", Value = "Critical" }
            }, "Value", "Text");


            ViewBag.CriticalityList = CriticalityList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAppointment([Bind(Include = "AppointmentID,PhysicianID,AppointmentDateTime,Criticality,Reason,Note")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var curr_usr = Session["CurrentUser"] as CurrentUser;

                //int isExist = IsPatientAppointmentLimit(appointment.AppointmentDateTime, appointment.PhysicianID, (int)curr_usr.ReferenceToID);
                //int  isExist = IsPhysicianAppointmentClear(appointment.AppointmentDateTime, appointment.PhysicianID);


                //                Dictionary<int, string> errorMessages = new Dictionary<int, string>
                //{               { 0, "Physician is not Available! Please choose a time that is 20 minutes later." },
                //                { 1, "Appointment Limit Reached! Please choose appointment for the next day." }
                // Add more mappings if needed
                //};
                appointment.PatientID = (int)curr_usr.ReferenceToID;
                appointment.ScheduleStatus = "PENDING";
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var physicianList = db.Physicians.Select(p => new
            {
                p.PhysicianID,
                FullName = p.PhysicianName + " (" + p.Specialization + ")"
            }).ToList();
            ViewBag.PhysicianID = new SelectList(physicianList, "PhysicianID", "Fullname");
            SelectList CriticalityList = new SelectList(new[]
                {
                new SelectListItem { Text = "Normal", Value = "Normal" },
                new SelectListItem { Text = "Critical", Value = "Critical" }
            }, "Value", "Text");


            ViewBag.CriticalityList = CriticalityList;

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

        public ActionResult DisplayPatientPrescription(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<PhysicianPrescription> physicianPrescription = db.PhysicianPrescriptions.Where(a => a.ScheduleID == id).ToList();
            if (physicianPrescription == null)
            {
                return View(physicianPrescription);
            }
            return View(physicianPrescription);
        }

        //[NonAction]
        //public int IsPhysicianAppointmentClear(DateTime appointmentDatetime , int physicianID)
        //{
        //    DateTime rangeStart = appointmentDatetime.AddMinutes(-5);
        //    DateTime rangeEnd = appointmentDatetime.AddMinutes(5);
        //    using (ClinicAutomationEntities _db = new ClinicAutomationEntities())
        //    {

        //        var appointmentAfter = db.Appointments
        //    .Where(a => a.PhysicianID == physicianID)
        //    .Where(record =>record.AppointmentDateTime > rangeStart && record.AppointmentDateTime <rangeEnd).ToList();
        //    //    var appointmentAfter = _db.Appointments
        //    //.Where(a => a.PhysicianID == physicianID
        //    //         && (a.AppointmentDateTime >= rangeStart
        //    //         && a.AppointmentDateTime < appointmentDatetime));

        //        if (appointmentAfter.Count != 0)
        //            return 0;

        //        return -1;
        //    }
        //}



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

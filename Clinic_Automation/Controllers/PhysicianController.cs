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
    public class PhysicianController : Controller
    {
        // GET: Physician
        private ClinicAutomationEntities db = new ClinicAutomationEntities();
        [Authorize(Roles = "PHYSICIAN")]

        public ActionResult Index()
        {

            var curr_usr = Session["CurrentUser"] as CurrentUser;

            if (curr_usr.ReferenceToID == null)
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
        public ActionResult DisplayPhysicianPrescription(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<PhysicianPrescription> physicianPrescription = db.PhysicianPrescriptions.Where(a => a.ScheduleID == id).ToList();
            if (physicianPrescription == null)
            {
                return HttpNotFound();
            }
            return View(physicianPrescription);
        }


        public ActionResult CreatePhysicianPrescription()
        {

            ViewBag.ScheduleID = new SelectList(db.Schedules, "ScheduleID", "ScheduleID");
            ViewBag.DrugID = new SelectList(db.Drugs, "DrugID", "DrugID");
            ViewBag.PhysicianAdviceID = new SelectList(db.PhysicianAdvices, "PhysicianAdviceID", "PhysicianAdviceID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePhysicianPrescription([Bind(Include = "PhysicianPrescriptionID,ScheduleID,DrugID,PhysicianAdviceID,Description")] PhysicianPrescription physicianPrescription)
        {
            if (ModelState.IsValid)
            {

                db.PhysicianPrescriptions.Add(physicianPrescription);
                db.SaveChanges();

                return RedirectToAction("DisplayPhysicianPrescription", new { id = physicianPrescription.ScheduleID });
            }

            return View(physicianPrescription);
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

        //public ActionResult PhysicianAdvice()
        //{
        //    var lst = db.PhysicianAdvices.ToList().Select(d => new Physician { PhysicianName = d.PhysicianAdvi, PhysicianID = d.PhysicianAdviceID }).ToList();

        //    ViewBag.SupplierID = new SelectList(db.Suppliers.ToList(), "SupplierID", "FirstName");
        //    ViewBag.DrugID = new SelectList(lst, "DrugID", "DrugName");
        //    ViewBag.DrugDataList = lst;


        //    return View();
        //}
        //[HttpPost]
        //public ActionResult PhysicianPrescription(Models.POViewModel vm)
        //{
        //    vm.POHeader.Supplier = _db.Suppliers.Find(int.Parse(Request.Form.Get("SupplierID")));


        //    vm.POProductLines.ToList().ForEach(pl => { vm.POHeader.PurchaseOrderProductLines.Add(pl); });
        //    _db.PurchaseOrderHeaders.Add(vm.POHeader);

        //    _db.SaveChanges();


        //    return RedirectToAction("Index");
        //}


    }

}
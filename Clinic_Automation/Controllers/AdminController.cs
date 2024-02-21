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










        //--------------------------PHYSICIAN CRUD OPERATIONS---------

        public ActionResult DisplayAdminPhysician()
        {
            return View(_db.Physicians.ToList());
        }


        public ActionResult DetailsAdminPhysician(int? id)
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
























        //--------------------------PATIENT CRUD OPERATIONS---------

            
        public ActionResult DisplayAdminPatient()
        {
            return View(_db.Patients.ToList());
        }

        public ActionResult DetailsAdminPatient(int? id)
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
                usr.Password = patient.PatientName + rnd.Next(1000, 10000) + "@" + "B" + "2";
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

























        //--------------------------CHEMIST CRUD OPERATIONS---------

        public ActionResult DisplayAdminChemist()
        {
            return View(_db.Chemists.ToList());
        }

        public ActionResult DetailsAdminChemist(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chemist chemist = _db.Chemists.Find(id);
            if (chemist == null)
            {
                return HttpNotFound();
            }
            return View(chemist);
        }
        public ActionResult CreateAdminChemist()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdminChemist([Bind(Include = "ChemistID,ChemistName,ChemistAddress,ChemistNumber,ChemistEmailID,ChemistSummary")] Chemist chemist)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                _db.Chemists.Add(chemist);
                _db.SaveChanges();

                User usr = new User();
                usr.ReferenceToID = chemist.ChemistID;
                usr.UserName = chemist.ChemistName + rnd.Next(1000, 10000);
                usr.Password = chemist.ChemistID + rnd.Next(1000, 10000) + "@" + "C" + "3";
                usr.Role = "CHEMIST";

                _db.Users.Add(usr);
                _db.SaveChanges();
                return RedirectToAction("DisplayAdminChemist");
            }

            return View(chemist);
        }


        public ActionResult EditAdminChemist(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chemist chemist = _db.Chemists.Find(id);
            if (chemist == null)
            {
                return HttpNotFound();
            }
            return View(chemist);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdminChemist([Bind(Include = "ChemistID,ChemistName,ChemistAddress,ChemistNumber,ChemistEmailID,ChemistSummary")] Chemist chemist)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(chemist).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("DisplayAdminChemist");
            }
            return View(chemist);
        }


        public ActionResult DeleteAdminChemist(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chemist chemist = _db.Chemists.Find(id);
            if (chemist == null)
            {
                return HttpNotFound();
            }
            return View(chemist);
        }

        [HttpPost, ActionName("DeleteAdminChemist")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedAdminChemist(int id)
        {
            Chemist chemist = _db.Chemists.Find(id);
            _db.Chemists.Remove(chemist);
            _db.SaveChanges();
            return RedirectToAction("DisplayAdminChemist");
        }

















        //--------------------------SUPPLIER CRUD OPERATIONS---------


        public ActionResult DisplayAdminSupplier()
        {
            return View(_db.Suppliers.ToList());
        }

        public ActionResult DetailsAdminSupplier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = _db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        public ActionResult CreateAdminSupplier()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdminSupplier([Bind(Include = "SupplierID,SupplierName,SupplierAddress,SupplierNumber,SupplierEmailID")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                _db.Suppliers.Add(supplier);
                _db.SaveChanges();
                User usr = new User();
                usr.ReferenceToID = supplier.SupplierID;
                usr.UserName = supplier.SupplierName + rnd.Next(1000, 10000);
                usr.Password = supplier.SupplierName+ rnd.Next(1000, 10000) + "@" + "D" + "4";
                usr.Role = "SUPPLIER";

                _db.Users.Add(usr);
                _db.SaveChanges();
                return RedirectToAction("DisplayAdminSupplier");
            }

            return View(supplier);
        }


        public ActionResult EditAdminSupplier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = _db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdminSupplier([Bind(Include = "SupplierID,SupplierName,SupplierAddress,SupplierNumber,SupplierEmailID")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(supplier).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("DisplayAdminSupplier");
            }
            return View(supplier);
        }


        public ActionResult DeleteAdminSupplier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = _db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        [HttpPost, ActionName("DeleteAdminSupplier")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedAdminSupplier(int id)
        {
            Supplier supplier = _db.Suppliers.Find(id);
            _db.Suppliers.Remove(supplier);
            _db.SaveChanges();
            return RedirectToAction("DisplayAdminSupplier");
        }









        //------------------------------Schedule Appointment-----------------------------------------

        public ActionResult DisplayAdminAppointment()
        {
            var appointments = _db.Appointments.Include(a => a.Patient).Include(a => a.Physician);
            return View(appointments.ToList());
        }
        public ActionResult EditAdminAppointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = _db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientID = new SelectList(_db.Patients, "PatientID", "PatientName", appointment.PatientID);
            ViewBag.PhysicianID = new SelectList(_db.Physicians, "PhysicianID", "PhysicianName", appointment.PhysicianID);
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdminAppointment([Bind(Include = "AppointmentID,PatientID,PhysicianID,AppointmentDateTime,Criticality,Reason,Note,ScheduleStatus")] Appointment editedAppointment)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(editedAppointment).State = EntityState.Modified;
                _db.SaveChanges();
                if (editedAppointment.ScheduleStatus == "APPROVED")
                {
                    Schedule schedule = new Schedule();
                    schedule.AppointmentID = editedAppointment.AppointmentID;
                    schedule.ScheduleDate = editedAppointment.AppointmentDateTime;
                    schedule.ScheduleStatus = "APPROVED";
                    _db.Schedules.Add(schedule);
                    _db.SaveChanges();
                }
                return RedirectToAction("DisplayAdminAppointment");

            }
            ViewBag.PatientID = new SelectList(_db.Patients, "PatientID", "PatientName", editedAppointment.PatientID);
            ViewBag.PhysicianID = new SelectList(_db.Physicians, "PhysicianID", "PhysicianName", editedAppointment.PhysicianID);
            return View(editedAppointment);
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

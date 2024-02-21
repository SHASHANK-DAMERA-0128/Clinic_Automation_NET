using Clinic_Automation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Automation.Controllers
{
    public class SupplierController : Controller
    {
        private ClinicAutomationEntities _db = new ClinicAutomationEntities();
        // GET: Supplier

        public ActionResult Index()
        {
            var curr_usr = Session["CurrentUser"] as CurrentUser;
            if (curr_usr.ReferenceToID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<PurchaseOrderHeader> POH = _db.PurchaseOrderHeaders.Where(a => a.SupplierID == curr_usr.ReferenceToID).ToList();
            if (POH.Count == 0)
            {
                return View(POH);
            }
            return View(POH);
        }

        public ActionResult DisplayProductLine(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var POL = _db.PurchaseProductLines.Where(a => a.PurchaseOrderID == id).ToList();



            PurchaseProductLine ppl = _db.PurchaseProductLines.Find(id);
            //ViewBag.DrugId = new SelectList(_db.Drugs, "DrugID", "Title", ppl.DrugId);
            //if (POL == null)
            //{
            //    return View(POL);
            //}
            return View(POL);
           
        }

        public ActionResult DisplayAdminSupplier(int? id)
        {

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //var POH = _db.PurchaseOrderHeaders.Where(a => a.SupplierID == id).ToList();
            //if(POH.Count == 0)
            //{
            //    return View(POH);
            //}




            return View(_db.PurchaseProductLines.ToList());
        }




        //public ActionResult EditAdminSupplier(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Supplier supplier = _db.Suppliers.Find(id);
        //    if (supplier == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(supplier);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditAdminSupplier([Bind(Include = "SupplierID,SupplierName,SupplierAddress,SupplierNumber,SupplierEmailID")] Supplier supplier)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Entry(supplier).State = EntityState.Modified;
        //        _db.SaveChanges();
        //        return RedirectToAction("DisplayAdminSupplier");
        //    }
        //    return View(supplier);
        //}




    }
}


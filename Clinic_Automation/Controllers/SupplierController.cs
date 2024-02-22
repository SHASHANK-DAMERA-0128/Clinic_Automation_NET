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
            SelectList purchaseOrderStatusList = new SelectList(new[]
    {
        new SelectListItem { Text = "Pending", Value = "Pending" },
        new SelectListItem { Text = "Approved", Value = "Approved" },
        new SelectListItem { Text = "Rejected", Value = "Rejected" }
    }, "Value", "Text");

            
           

            PurchaseProductLine ppl = _db.PurchaseProductLines.Find(id);
            _db.SaveChanges();
            ViewBag.PurchaseOrderStatusList = purchaseOrderStatusList;
            return View(POL);
           
        }

        [HttpPost]
        public ActionResult DisplayProductLine(int? id, string selectedStatus)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Retrieve the purchase product header associated with the given ID
            var purchaseProductHeader = _db.PurchaseOrderHeaders.Find(id);

            if (purchaseProductHeader == null)
            {
                return HttpNotFound(); // Or any other appropriate action if the purchase product header is not found
            }

            // Update the status column in the PurchaseProductHeader table
            purchaseProductHeader.PurchaseOrderStatus = selectedStatus;

            // Save changes to the database
            _db.SaveChanges();

            // Redirect to some action after saving changes
            return RedirectToAction("Index"); // Replace "Index" with the appropriate action
        }

        public ActionResult DisplayAdminSupplier(int? id)
        {




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


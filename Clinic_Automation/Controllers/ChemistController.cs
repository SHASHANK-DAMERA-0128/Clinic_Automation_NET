using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clinic_Automation.Models;

namespace Clinic_Automation.Controllers
{

    public class ChemistController : Controller
    {
        private ClinicAutomationEntities db = new ClinicAutomationEntities();
        // GET: Chemist
        [Authorize(Roles = "Chemist")]
        // Index method begins here
        // ----------------------------------------------
        public ActionResult Index(int? page, string filter = "Not", int pageSize = 5)
        {
            int pageNumber = (page ?? 1);

            IQueryable<DrugRequest> query;
            if (filter == "All") query = db.DrugRequests;
            else query = db.DrugRequests.Where(d => d.RequestStatus == filter);

            int totalItems = query.Count();

            var drugRequests = query
                .OrderByDescending(d => d.RequestedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.Filter = filter;

            return View(drugRequests);
        }

     //    [HttpPatch]
        public ActionResult ToggleStatus(int id)
        {
            var drugRequest = db.DrugRequests.Find(id);
            if (drugRequest != null)
            {
                drugRequest.RequestStatus = (drugRequest.RequestStatus == "Not") ? "Noted" : "Not";
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult ToggleFilter(string currentFilter)
        {
            string filter = currentFilter == "Not" ? "All" : "Not";
            ViewBag.Filter = filter;
            return RedirectToAction("Index", new { filter });
        }

        // ----------------------------------------------

        public ActionResult PlacePurchaseOrder()
        {
            var lst = db.Drugs.ToList().Select(d => new DrugModel { DrugName = d.Title, DrugID = d.DrugID }).ToList();

            ViewBag.SupplierID = new SelectList(db.Suppliers.ToList(), "SupplierID", "SupplierName");
            ViewBag.DrugID = new SelectList(lst, "DrugID", "DrugName");
            ViewBag.DrugDataList = lst;

            return View();
        }

        [HttpPost]
        public ActionResult PlacePurchaseOrder(POViewModel vm)
        {
            //int supplierID = int.Parse(Request.Form.Get("SupplierID"));
            //Supplier supplier = db.Suppliers.Find(supplierID);

            //vm.POHeader.Supplier = supplier;

            //vm.POProductLines.ToList().ForEach(pl => { vm.POHeader.PurchaseProductLines.Add(pl); });

            //db.PurchaseOrderHeaders.Add(vm.POHeader);
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    foreach (var validationError in ex.EntityValidationErrors)
            //    {
            //        foreach (var error in validationError.ValidationErrors)
            //        {
            //            Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //                validationError.Entry.Entity.GetType().Name,
            //                validationError.Entry.State);
            //            Debug.WriteLine("- Property: {0}, Error: {1}",
            //                error.PropertyName,
            //                error.ErrorMessage);
            //        }
            //    }
            //}

            vm.POHeader.Supplier = db.Suppliers.Find(int.Parse(Request.Form.Get("SupplierID")));

            vm.POProductLines.ToList().ForEach(pl => { vm.POHeader.PurchaseProductLines.Add(pl); });

            db.PurchaseOrderHeaders.Add(vm.POHeader);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
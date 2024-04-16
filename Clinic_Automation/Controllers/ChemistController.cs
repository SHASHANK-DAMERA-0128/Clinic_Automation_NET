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
        [Authorize(Roles = "Chemist, CHEMIST")]

        public ActionResult Index(int? page, string search = "", string filter = "Not", int pageSize = 5)
        {
            int pageNumber = (page ?? 1);

            IQueryable<DrugRequest> query;
            if (filter == "All") query = db.DrugRequests;
            else query = db.DrugRequests.Where(d => d.RequestStatus == filter);

            query = query.Where(d => d.DrugInfoText.Contains(search)
            || d.RequestedDate.ToString().Contains(search));

            var drugRequests = query
                .OrderByDescending(d => d.RequestedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalItems = query.Count();

            int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.Filter = filter;
            ViewBag.search = search;

            return View(drugRequests);
        }

        // [HttpPatch]
        public ActionResult ToggleStatus(int? id, string search = "", string filter = "Not", int page = 1, int pageSize = 5)
        {
            var drugRequest = db.DrugRequests.Find(id);
            if (drugRequest != null)
            {
                drugRequest.RequestStatus = (drugRequest.RequestStatus == "Not") ? "Noted" : "Not";
                db.SaveChanges();
            }
            // redirect to Action Index with the same page number, filter and page size
            return RedirectToAction("Index", new { page, filter, pageSize });
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

            vm.POHeader.Supplier = db.Suppliers.Find(int.Parse(Request.Form.Get("SupplierID")));

            vm.POProductLines.ToList().ForEach(pl => { vm.POHeader.PurchaseProductLines.Add(pl); });
            vm.POHeader.PurchaseOrderStatus = "Pending";
            db.PurchaseOrderHeaders.Add(vm.POHeader);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // -----------------------------------------------------------------
        public ActionResult ViewOrders()
        {
            var orders = db.PurchaseOrderHeaders.ToList();
            return View(orders);
        }

        public ActionResult ViewOrderDetails(int? id)
        {
            try
            {
                var supplierDetails = db.PurchaseOrderHeaders.Find(id);
                var order = db.PurchaseProductLines.Where(p => p.PurchaseOrderID == id).ToList();
                ViewBag.Supplier = supplierDetails;

                return View(order);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("ViewOrders");
            }
        }
    }
}
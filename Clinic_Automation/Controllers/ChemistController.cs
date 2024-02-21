using System;
using System.Collections.Generic;
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
            return View();
        }

        [HttpPost]
        public ActionResult PlacePurchaseOrder(string s)
        {
            return View();
        }
    }
}
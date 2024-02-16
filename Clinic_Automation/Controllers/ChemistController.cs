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
        [Authorize(Roles = "Chemist, CHEMIST")]
        public ActionResult Index(int? page, string filter, int pageSize = 5)
        {
            if (filter == null)
                filter = "Noted";

            int pageNumber = (page ?? 1);

            int totalItems =
                db.DrugRequests
                .Where(d => d.RequestStatus == filter)
                .Count();

            var drugRequests = db.DrugRequests
                .Where(d => d.RequestStatus == filter)
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
            string filter = currentFilter == "Not" ? "Noted" : "Not";

            ViewBag.Filter = filter;

            return RedirectToAction("Index", new { filter });
        }
    }
}
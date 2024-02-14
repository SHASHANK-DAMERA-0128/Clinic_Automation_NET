using Clinic_Automation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Clinic_Automation.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if(ModelState.IsValid)
            {
                Models.ClinicAutomationEntities _db = new Models.ClinicAutomationEntities();
                User usr = _db.Users.SingleOrDefault(dbusr => dbusr.UserName.ToLower() == user.UserName.ToLower()
                && dbusr.Password.ToLower() == user.Password.ToLower());    

                if (usr != null)
                {
                    FormsAuthentication.SetAuthCookie(usr.UserName, false);
                    return RedirectToAction("Index",usr.Role);
                }

            }

            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            Models.ClinicAutomationEntities _db = new Models.ClinicAutomationEntities();


            if (ModelState.IsValid)
            {
            }
            return View();
        }

        // User: Logout
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();                            
            return RedirectToAction("Index", "Home");
        }

    }
}
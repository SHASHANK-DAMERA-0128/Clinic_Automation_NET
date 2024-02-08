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
        public ActionResult Login(userTable user)
        {
            if(ModelState.IsValid)
            {
                Models.userEntities1 _db = new Models.userEntities1();
                userTable usr = _db.userTables.SingleOrDefault(dbusr => dbusr.User_Name.ToLower() == user.User_Name.ToLower()
                && dbusr.Password.ToLower() == user.Password.ToLower());    

                if (usr != null)
                {
                    FormsAuthentication.SetAuthCookie(usr.User_Name, false);
                    return RedirectToAction("Index",usr.Role);
                }

            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }
    }
}
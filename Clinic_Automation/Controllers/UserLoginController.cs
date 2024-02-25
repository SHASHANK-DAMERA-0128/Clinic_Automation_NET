using Clinic_Automation.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;


namespace Clinic_Automation.Controllers
{
    public class UserLoginController : Controller
    {
        Models.ClinicAutomationEntities _db = new Models.ClinicAutomationEntities();

        // GET: UserLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            
            
            if (ModelState.IsValid)
            {
                var usr = _db.Users.Where(a => a.UserName == user.UserName).FirstOrDefault();
                if (usr != null)
                {
                    if (string.Compare(crypto.Encrypt(user.Password), usr.Password) == 0)
                    {
                        FormsAuthentication.SetAuthCookie(usr.UserName, false);
                        CurrentUser currentUser = new CurrentUser();
                        currentUser.UserName = usr.UserName;
                        currentUser.ReferenceToID = usr.ReferenceToID;
                        currentUser.UserID = usr.UserID;
                        currentUser.Role = usr.Role;

                        Session["CurrentUser"] = currentUser;
                        return RedirectToAction("Index", usr.Role);
                    }
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
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "PatientID,PatientName,DOB,PatientNumber,PatientEmail,PatientAddress,Gender,PatientSummary")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                var isExistnumber = IsPhonenumberExist(patient.PatientNumber);
                if (isExistnumber)
                {
                    ModelState.AddModelError("PhoneNumberExists", "Phone Number Already exist");
                    return View(patient);
                }
                var isExist = IsEmailExist(patient.PatientEmail);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExists", "Email Already exist");
                    return View(patient);
                }
                _db.Patients.Add(patient);
                _db.SaveChanges();
                return RedirectToAction("PatientLoginSetup", new { Patientid = patient.PatientID, Patientemail = patient.PatientEmail });
            }
            return View(patient);
        }
        public ActionResult PatientLoginSetup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientLoginSetup([Bind(Include = "UserName,Password")] User user, int? Patientid, string Patientemail)
        {
            if (ModelState.IsValid)
            {
                Patient patient = new Patient();
                user.Password = crypto.Encrypt(user.Password);
                user.ReferenceToID = Patientid;
                user.Role = "PATIENT";
                _db.Users.Add(user);
                _db.SaveChanges();
                //var ac = Guid.NewGuid();
                //SendVerificationLinkEmail(Patientemail, ac);
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
        [NonAction]
        public bool IsEmailExist(String emailID)
        {
            using (ClinicAutomationEntities _db = new ClinicAutomationEntities())
            {
                var v = _db.Patients.Where(a => a.PatientEmail == emailID).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]
        public bool IsPhonenumberExist(String phone)
        {
            using (ClinicAutomationEntities _db = new ClinicAutomationEntities())
            {
                var v = _db.Patients.Where(a => a.PatientNumber == phone).FirstOrDefault();
                return v != null;
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }




        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string username)
        {
            var user = _db.Users.SingleOrDefault(u => u.UserName == username);
            if (user != null)
            {
                //return RedirectToAction("ResetPassword");
                return RedirectToAction("ResetPassword", new { userName = username });
            }
            ModelState.AddModelError("Username", "Username not found");
            return View();
        }


        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword([Bind(Include = "Password")] User user, String userName)
        {
            var usr = _db.Users.SingleOrDefault(u => u.UserName == userName);
            usr.Password = crypto.Encrypt(user.Password);
            _db.Entry(usr).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Login");

        }
    }
}


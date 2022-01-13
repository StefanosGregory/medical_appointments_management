using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalAppointmentsManagement.Models;

namespace MedicalAppointmentsManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(PATIENT objUser)
        {
            if (ModelState.IsValid)
            {
                using (MedicalDBEntities db = new MedicalDBEntities())
                {
                    var obj = db.PATIENTs.Where(a => a.username.Equals(objUser.username) && a.hash.Equals(objUser.hash)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.userid.ToString();
                        Session["UserName"] = obj.username.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
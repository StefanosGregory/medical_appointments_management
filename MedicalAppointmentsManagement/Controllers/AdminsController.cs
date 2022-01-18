using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalAppointmentsManagement.Models;

namespace MedicalAppointmentsManagement.Controllers
{
    public class AdminsController : Controller
    {
        private MedicalDBEntities db = new MedicalDBEntities();

        // Login
        // GET: Admins/Index -> redirect to login
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        // Login
        // GET: Admins/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Admins/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ADMIN objUser)
        {

            using (MedicalDBEntities db = new MedicalDBEntities())
            {
                var obj = db.ADMINs.Where(a => a.username.Equals(objUser.username) && a.password.Equals(objUser.password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["admin"] = obj.userid.ToString();
                    Session["UserName"] = obj.username.ToString();
                    return RedirectToAction("Menu");
                }
            }
            ViewData["Error"] = "Error";
            return View(objUser);
        }

        // GET: Admins/Menu
        public ActionResult Menu()
        {
            if (Session["admin"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // Admins/Logout
        public ActionResult Logout()
        {
            Session["admin"] = null;
            Session["UserName"] = null;
            Session.Abandon();
            return Redirect("../Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

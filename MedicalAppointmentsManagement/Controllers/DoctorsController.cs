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
    public class DoctorsController : Controller
    {
        private MedicalDBEntities db = new MedicalDBEntities();


        // Login
        // GET: Doctors/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Doctors/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(DOCTOR objUser)
        {

            using (MedicalDBEntities db = new MedicalDBEntities())
            {
                var obj = db.DOCTORs.Where(a => a.username.Equals(objUser.username) && a.password.Equals(objUser.password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["doctorAMKA"] = obj.doctorAMKA.ToString();
                    Session["UserName"] = obj.username.ToString();
                    return RedirectToAction("Menu");
                }
            }
            ViewData["Error"] = "Error";
            return View(objUser);
        }

        // GET: Doctors/Menu
        public ActionResult Menu()
        {
            if (Session["doctorAMKA"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session["doctorAMKA"] = null;
            Session["UserName"] = null;
            Session.Abandon();
            return Redirect("../Home");
        }


        // GET: Index -> login
        public ActionResult Index()
        {
            return RedirectToAction("Menu");
        }


        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["doctorAMKA"] == null)
            {
                return RedirectToAction("Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCTOR dOCTOR = db.DOCTORs.Find(id);
            if (dOCTOR == null)
            {
                return HttpNotFound();
            }
            return View(dOCTOR);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["doctorAMKA"] == null || !Session["doctorAMKA"].ToString().Equals(id.ToString()))
            {
                return RedirectToAction("Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCTOR dOCTOR = db.DOCTORs.Find(id);
            if (dOCTOR == null)
            {
                return HttpNotFound();
            }
            return View(dOCTOR);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "doctorAMKA,username,name,surname,specialty,password")] DOCTOR doctor)
        {
            if (Session["doctorAMKA"] == null || !Session["doctorAMKA"].ToString().Equals(doctor.doctorAMKA.ToString()))
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("~/Doctors/Details/"+doctor.doctorAMKA);
            }
            return View(doctor);
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

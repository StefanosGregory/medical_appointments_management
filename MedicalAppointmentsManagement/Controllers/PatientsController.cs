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
    public class PatientsController : Controller
    {
        private MedicalDBEntities db = new MedicalDBEntities();

        // Login
        // GET: Patients/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Patients/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(PATIENT objUser)
        {

                using (MedicalDBEntities db = new MedicalDBEntities())
                {
                    var obj = db.PATIENTs.Where(a => a.username.Equals(objUser.username) && a.password.Equals(objUser.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserAMKA"] = obj.patientAMKA.ToString();
                        Session["UserName"] = obj.username.ToString();
                        return RedirectToAction("Menu");
                    }
                }
                ViewData["Error"] = "Please check your email and password!";
                return View(objUser);
        }

        // GET: Patients/Menu
        public ActionResult Menu()
        {
            if (Session["UserAMKA"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //Logout

        public ActionResult Logout()
        {
            Session["UserAMKA"] = null;
            Session["UserName"] = null;
            Session.Abandon();
            return Redirect("../Home");
        }


        // GET: Patients
        public ActionResult Index()
        {
            return View(db.PATIENTs.ToList());
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PATIENT pATIENT = db.PATIENTs.Find(id);
            if (pATIENT == null)
            {
                return HttpNotFound();
            }
            return View(pATIENT);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "patientAMKA,userid,username,name,surname,password")] PATIENT patient)
        {
            if (ModelState.IsValid)
            {
                db.PATIENTs.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PATIENT pATIENT = db.PATIENTs.Find(id);
            if (pATIENT == null)
            {
                return HttpNotFound();
            }
            return View(pATIENT);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "patientAMKA,userid,username,name,surname,password")] PATIENT patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Error"] = "Please check your infos!";
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PATIENT pATIENT = db.PATIENTs.Find(id);
            if (pATIENT == null)
            {
                return HttpNotFound();
            }
            return View(pATIENT);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PATIENT pATIENT = db.PATIENTs.Find(id);
            db.PATIENTs.Remove(pATIENT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose(); // Connections close
            }
            base.Dispose(disposing);
        }
    }
}

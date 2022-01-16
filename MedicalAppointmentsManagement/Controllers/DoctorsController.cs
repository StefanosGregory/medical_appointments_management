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
            if (Session["doctorAMKA"] != null)
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
            Session["doctorAMKA"] = null;
            Session["UserName"] = null;
            Session.Abandon();
            return Redirect("../Home");
        }


        // GET: Doctors
        public ActionResult Index()
        {
            return View(db.DOCTORs.ToList());
        }


        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: Doctors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "doctorAMKA,username,name,surname,specialty,password")] DOCTOR dOCTOR)
        {
            if (ModelState.IsValid)
            {
                db.DOCTORs.Add(dOCTOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dOCTOR);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
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
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("~/Doctors/Details/"+doctor.doctorAMKA);
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DOCTOR dOCTOR = db.DOCTORs.Find(id);
            db.DOCTORs.Remove(dOCTOR);
            db.SaveChanges();
            return RedirectToAction("Index");
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

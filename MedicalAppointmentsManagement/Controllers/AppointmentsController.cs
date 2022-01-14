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
    public class AppointmentsController : Controller
    {
        private MedicalDBEntities db = new MedicalDBEntities();

        // GET: Appointments
        public ActionResult Index()
        {
            var aPPOINTMENTs = db.APPOINTMENTs.Include(a => a.DOCTOR).Include(a => a.PATIENT);
            return View(aPPOINTMENTs.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPOINTMENT aPPOINTMENT = db.APPOINTMENTs.Find(id);
            if (aPPOINTMENT == null)
            {
                return HttpNotFound();
            }
            return View(aPPOINTMENT);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.DOCTOR_doctorAMKA = new SelectList(db.DOCTORs, "doctorAMKA", "username");
            ViewBag.PATIENT_patientAMKA = new SelectList(db.PATIENTs, "patientAMKA", "username");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "date,startSlotTime,endSlotTime,PATIENT_patientAMKA,DOCTOR_doctorAMKA,isAvailable")] APPOINTMENT aPPOINTMENT)
        {
            if (ModelState.IsValid)
            {
                db.APPOINTMENTs.Add(aPPOINTMENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DOCTOR_doctorAMKA = new SelectList(db.DOCTORs, "doctorAMKA", "username", aPPOINTMENT.DOCTOR_doctorAMKA);
            ViewBag.PATIENT_patientAMKA = new SelectList(db.PATIENTs, "patientAMKA", "username", aPPOINTMENT.PATIENT_patientAMKA);
            return View(aPPOINTMENT);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPOINTMENT aPPOINTMENT = db.APPOINTMENTs.Find(id);
            if (aPPOINTMENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.DOCTOR_doctorAMKA = new SelectList(db.DOCTORs, "doctorAMKA", "username", aPPOINTMENT.DOCTOR_doctorAMKA);
            ViewBag.PATIENT_patientAMKA = new SelectList(db.PATIENTs, "patientAMKA", "username", aPPOINTMENT.PATIENT_patientAMKA);
            return View(aPPOINTMENT);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "date,startSlotTime,endSlotTime,PATIENT_patientAMKA,DOCTOR_doctorAMKA,isAvailable")] APPOINTMENT aPPOINTMENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aPPOINTMENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DOCTOR_doctorAMKA = new SelectList(db.DOCTORs, "doctorAMKA", "username", aPPOINTMENT.DOCTOR_doctorAMKA);
            ViewBag.PATIENT_patientAMKA = new SelectList(db.PATIENTs, "patientAMKA", "username", aPPOINTMENT.PATIENT_patientAMKA);
            return View(aPPOINTMENT);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPOINTMENT aPPOINTMENT = db.APPOINTMENTs.Find(id);
            if (aPPOINTMENT == null)
            {
                return HttpNotFound();
            }
            return View(aPPOINTMENT);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            APPOINTMENT aPPOINTMENT = db.APPOINTMENTs.Find(id);
            db.APPOINTMENTs.Remove(aPPOINTMENT);
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

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
            var appointments = db.APPOINTMENTs.Include(a => a.DOCTOR).Include(a => a.PATIENT).OrderBy(s => s.date);

            if (Session["UserAMKA"] != null)
            {
                int amka = Convert.ToInt32(Session["UserAMKA"]);
                appointments = appointments.Where(x => x.PATIENT_patient == amka).OrderBy(s => s.date);
                return View(appointments.ToList());
            }
            else if (Session["doctorAMKA"] != null)
            {
                int amka = Convert.ToInt32(Session["doctorAMKA"]);
                appointments = appointments.Where(x => x.DOCTOR_username == amka).OrderBy(s => s.date);
                return View(appointments.ToList());
            }
            else if (Session["admin"] != null)
            {
                return View(appointments.ToList());
            }

            return Redirect("~/Home");
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int id)
        {


            if (Session["UserAMKA"] == null && Session["doctorAMKA"] == null && Session["admin"] != null)
            {
                return Redirect("~/Home");
            }

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
            if (Session["UserAMKA"] == null && Session["doctorAMKA"] == null && Session["admin"] != null)
            {
                return Redirect("~/Home");
            }
            SelectListSet();
            return View();
        }

        private void SelectListSet()
        {
            var doctors = db.DOCTORs
                .Select(s => new
                {
                    Text = s.name + " " + s.surname,
                    Value = s.doctorAMKA
                })
                .ToList();

            var patients = db.PATIENTs
                .Select(s => new
                {
                    Text = s.name + " " + s.surname,
                    Value = s.patientAMKA
                })
                .ToList();

            ViewBag.DOCTOR_username = new SelectList(doctors, "Value", "Text");
            ViewBag.PATIENT_patient = new SelectList(patients, "Value", "Text");
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "date,startSlotTime,endSlotTime,PATIENT_patient,DOCTOR_username,isAvailable")] APPOINTMENT appointment)
        {
            if (Session["UserAMKA"] == null && Session["doctorAMKA"] == null && Session["admin"] != null)
            {
                return Redirect("~/Home");
            }

            if ((appointment.date < DateTime.Today)|| (appointment.date == DateTime.Today && appointment.startSlotTime <= DateTime.Now))
            {
                ViewData["Error"] = "Please select valid day!";
                SelectListSet();
                return View();
            }

            if (appointment.startSlotTime > Convert.ToDateTime("20:00:00") || appointment.startSlotTime < Convert.ToDateTime("09:00:00"))
            {
                ViewData["Error"] = "Please select working hours!";
                SelectListSet();
                return View();
            }

            appointment.endSlotTime = appointment.startSlotTime.AddHours(1);


            if (Session["UserAMKA"] != null)
            { 
                appointment.PATIENT_patient = Convert.ToInt32(Session["UserAMKA"]);
            }
            else if (Session["doctorAMKA"] != null)
            {
                appointment.DOCTOR_username = Convert.ToInt32(Session["doctorAMKA"]);
            }
            

            if (ModelState.IsValid)
            {
                if (db.APPOINTMENTs.Where(x =>
                        x.startSlotTime == appointment.startSlotTime && x.date == appointment.date &&
                        x.DOCTOR_username == appointment.DOCTOR_username).FirstOrDefault() == null)
                {
                    db.APPOINTMENTs.Add(appointment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewData["Error"] = "This time doctor is busy!";
                SelectListSet();
                return View();

            }
            ViewData["Error"] = "Check your inputs!";
            SelectListSet();
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["UserAMKA"] == null && Session["doctorAMKA"] == null && Session["admin"] == null)
            {
                return Redirect("~/Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPOINTMENT aPPOINTMENT = db.APPOINTMENTs.Find(id);
            if (aPPOINTMENT == null)
            {
                return HttpNotFound();
            }

            SelectListSet();
            return View(aPPOINTMENT);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,date,startSlotTime,endSlotTime,PATIENT_patient,DOCTOR_username,isAvailable")] APPOINTMENT appointment)
        {
            if (Session["UserAMKA"] == null && Session["doctorAMKA"] == null && Session["admin"] == null)
            {
                return Redirect("~/Home");
            }

            if ((appointment.date < DateTime.Today) || (appointment.date == DateTime.Today && appointment.startSlotTime <= DateTime.Now))
            {
                ViewData["Error"] = "Please select valid day!";
                SelectListSet();
                return View();
            }

            if (appointment.startSlotTime > Convert.ToDateTime("20:00:00") || appointment.startSlotTime < Convert.ToDateTime("09:00:00"))
            {
                ViewData["Error"] = "Please select working hours!";
                SelectListSet();
                return View();
            }

            appointment.endSlotTime = appointment.startSlotTime.AddHours(1);

            if (Session["UserAMKA"] != null)
            {
                appointment.PATIENT_patient = Convert.ToInt32(Session["UserAMKA"]);
            }
            else if (Session["doctorAMKA"] != null)
            {
                appointment.DOCTOR_username = Convert.ToInt32(Session["doctorAMKA"]);
            }

            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Error"] = "Please check your inputs!";
            SelectListSet();
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["UserAMKA"] == null && Session["doctorAMKA"] == null && Session["admin"] == null)
            {
                return Redirect("~/Home");
            }

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
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserAMKA"] == null && Session["doctorAMKA"] == null && Session["admin"] == null)
            {
                return Redirect("~/Home");
            }

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

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
            if (Session["UserName"] != null)
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
            Session["UserName"] = null;
            Session.Abandon();
            return Redirect("../Home");
        }




        // GET: Admins
        public ActionResult Index()
        {
            return View(db.ADMINs.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMIN aDMIN = db.ADMINs.Find(id);
            if (aDMIN == null)
            {
                return HttpNotFound();
            }
            return View(aDMIN);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userid,username,password")] ADMIN Admin)
        {
            if (ModelState.IsValid)
            {
                db.ADMINs.Add(Admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMIN aDMIN = db.ADMINs.Find(id);
            if (aDMIN == null)
            {
                return HttpNotFound();
            }
            return View(aDMIN);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userid,username,password")] ADMIN Admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMIN aDMIN = db.ADMINs.Find(id);
            if (aDMIN == null)
            {
                return HttpNotFound();
            }
            return View(aDMIN);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ADMIN aDMIN = db.ADMINs.Find(id);
            db.ADMINs.Remove(aDMIN);
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

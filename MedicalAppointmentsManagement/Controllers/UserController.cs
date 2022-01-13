using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalAppointmentsManagement.Models;

namespace MedicalAppointmentsManagement.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PATIENT obj)
        {
            if (ModelState.IsValid)
            {
                MedicalDBEntities db = new MedicalDBEntities();
                db.PATIENTs.Add(obj);
                db.SaveChanges();
            }
            return View(obj);
        }
    }
}
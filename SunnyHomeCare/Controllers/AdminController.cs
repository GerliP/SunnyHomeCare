using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SunnyHomeCare.Models;

namespace SunnyHomeCare.Controllers
{
    public class AdminController : Controller
    {
        private HomeCare db = new HomeCare();

        // GET: Admin
        public ActionResult Users()
        {

            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var users = db.Users.Include(u => u.Role);
                return View(users.ToList());
            };
        }

        public ActionResult Visits()
        {
            if(Session["Id"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var visits = db.Patients.ToList();
                return View("Patients", visits);
            }
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_Name = user.Role.Name;

            //Foo model = db.Foos.Where(x => x.ID == id).Include(x => x.Bar).FirstOrDefault();
            return View(user);

        }
        [ChildActionOnly]
        public PartialViewResult Contact()
        {
            return PartialView("PersonalContactPV"); // or return PartialView("_QuickContact", new ContactForm());
        }
        
        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.Role_id = new SelectList(db.Roles, "Id", "Name");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Firstname,Lastname,Email,Password,Address,ContactNumber,Role_id,Gender")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                ViewBag.Role_id = new SelectList(db.Roles, "Id", "Name", user.Role_id);
                if (user.Role_id == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (user.Role_id == 2)
                {

                    int id = user.Id;
                    System.Diagnostics.Debug.WriteLine("ÍDAfter: " + id);

                    return RedirectToAction("AdminPatientCreate", new { id });
                }

                else
                {
                    int id = user.Id;
                    System.Diagnostics.Debug.WriteLine("ÍDAfter: " + id);

                    return RedirectToAction("AdminCaretakerCreate", new { id });
                }
            }
            else
            {
                return View(user);

            }
        }

        public ActionResult AdminCaretakerCreate(int id)
        {

            ViewBag.UserId = id;
            return View();
        }
        [HttpPost]
        public ActionResult AdminCaretakerCreate([Bind(Include = "Id,User_id,Date_of_Employment")]Caretaker caretaker)
        {
            if (ModelState.IsValid)
            {
                db.Caretakers.Add(caretaker);
                db.SaveChanges();
                int id = caretaker.Id;
                ViewBag.Message = "Success";

                return RedirectToAction("Users");
            }
            else
            {
                ViewBag.CaretakerId = caretaker.Id;
                ViewBag.Message = "Failure!!";

                return View();
            }
          
        }

        public ActionResult AdminServiceContactCreate(int patientId)
        {
            ViewBag.PatientId = patientId;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminServiceContactCreate([Bind(Include = "Firstname,Lastname,Email,PhoneNumber,OtherInfo,JobTitle")]ServiceContact serviceContact, int patientId)
        {
            ViewBag.PatientId = patientId;

            if (ModelState.IsValid)
            {
                db.ServiceContacts.Add(serviceContact);
                db.SaveChanges();

                Patient patient = db.Patients.Find(patientId);
                patient.ServiceContacts.Add(serviceContact);
                db.SaveChanges();
                return View();
            }
            return View();

        }



        public ActionResult AdminPersonalContactCreate(int id)
        {
            ViewBag.PatientId = id; 
           
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminPersonalContactCreate([Bind(Include = "Id,Firstname,Lastname,PhoneNumber,Address,Email,Relation,OtherInfo")]PersonalContact personalContact, int patientId)

        {
            ViewBag.PatientId = patientId;

            if (ModelState.IsValid)
            {
                db.PersonalContacts.Add(personalContact);
                db.SaveChanges();

                Patient patient = db.Patients.Find(patientId);
                patient.PersonalContacts.Add(personalContact);
                db.SaveChanges();

                return View();
            }
            return View();

        }


        public ActionResult AdminPatientCreate(int id)
        {

            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminPatientCreate([Bind(Include = "Id,UserId,BloodType,Dislikes,Comments,Illness,Handicap")]Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                int id = patient.Id;
                ViewBag.Message = "Success";

                return RedirectToAction("AdminPersonalContactCreate", new { id });
            }
            else
            {
                ViewBag.PatientId = patient.Id;
                ViewBag.Message = "Failure!!";

                return View();
            }

        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_id = new SelectList(db.Roles, "Id", "Name", user.Role_id);
            return View(user);
        }
    


        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Firstname,Lastname,Email,Password,Address,ContactNumber,Role_id,Gender")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role_id = new SelectList(db.Roles, "Id", "Name", user.Role_id);
            return View(user);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

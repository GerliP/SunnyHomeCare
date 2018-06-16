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
    public class CaretakerController : Controller
    {
        private HomeCare db = new HomeCare();

        // GET: Caretaker
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                User user = db.Users.Find(Session["Id"]);
                return View("Details", user);
            }
        }

        // GET: Caretaker/Details/5
        public ActionResult Details(int? id)
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

        public PartialViewResult PatientDetails(Patient patient)
        {
            return PartialView("PatientPV", patient);
        }
        // Displaying @Html.Action("ActionName","ControllerName", ObjectValue)
        // Returns PartialViews for each instance of Personal Contact in the patient that is selected
        // This code snippets can be reused and require small changes, can probably be made into a interface
        [ChildActionOnly]
        public PartialViewResult PersonalContact(Patient patient)
        {
            List<PersonalContact> pc = patient.PersonalContacts.ToList();

            return PartialView("PersonalContactListPV", pc);
        }
        // Displaying @Html.Action("ActionName","ControllerName", ObjectValue)
        // Returns PartialViews for each instance of Service Contact in the patient that is selected
        // This code snippets can be reused and require small changes, can probably be made into a interface
        [ChildActionOnly]
        public PartialViewResult ServiceContact(Patient patient)
        {
            List<ServiceContact> sc = patient.ServiceContacts.ToList();

            return PartialView("ServiceContactListPV", sc);
        }

        public ActionResult PatientInfo(int? id)
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
            ViewBag.Id = user.Id;

            return View(user);

        }
    }
}

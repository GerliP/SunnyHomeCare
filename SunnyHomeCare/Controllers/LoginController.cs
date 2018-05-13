using SunnyHomeCare.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data;

namespace SunnyHomeCare.Controllers
{
    public class LoginController : Controller
    {
        private HomeCare db = new HomeCare();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(SunnyHomeCare.Models.User user)
        {

            var userDetails = db.Users.Where(m => m.Email == user.Email && m.Password == user.Password).FirstOrDefault();
            if (userDetails == null)
            {
                ViewBag.LoginErrorMessage = "Incorrect email or password";
                return View("Login");
            }
            else
            {
                Session["Id"] = userDetails.Id;
                Session["Firstname"] = userDetails.Firstname;
                int role = userDetails.Role_id;

                return FindView(role);

            }
        }
            public ActionResult FindView(int role)
            {
                if (role == 1) // Admin
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (role == 2) // Patinet
                {
                    return RedirectToAction("Index", "Patient");
                }
                else
                {
                    return RedirectToAction("Index", "Caretaker"); //Caretaker
                }
            }
            public ActionResult Logout(User user)
            {
                int userId = (int)Session["Id"];
                Session.Abandon();
                return RedirectToAction("Login", "Login");

            }
        }
    }
    

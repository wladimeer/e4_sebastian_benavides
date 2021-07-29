using Principal.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Principal.Controllers
{
    public class SessionController : Controller
    {
        private RepairEverythingEntities assistant = new RepairEverythingEntities();

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string email, string password)
        {
            try
            {
                var user = (from u in assistant.Users where u.email.Equals(email) && u.password.Equals(password) select u).FirstOrDefault();

                if (user == null)
                {
                    ViewBag.password = password; ViewBag.email = email;
                    TempData["error"] = "Verifica los Datos Ingresados.";
                    return View();
                }

                Session["user"] = user;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["error"] = "Error de Conexión.";
                return View();
            }
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session["user"] = null; Session.Abandon();
            return RedirectToAction("SignIn", "Session");
        }
    }
}
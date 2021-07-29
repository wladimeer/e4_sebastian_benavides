using System;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult Unauthorized(String operation, String module, String message)
        {
            ViewBag.message = message;
            ViewBag.operation = operation;
            ViewBag.module = module;
            return View();
        }
    }
}
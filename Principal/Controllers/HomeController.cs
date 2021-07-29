using Principal.Filters;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [UserAuthorize(operationId: 1)]
        public ActionResult Index()
        {
            ViewBag.user = Session["user"];
            return View();
        }
    }
}
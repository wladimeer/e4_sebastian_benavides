using Principal.Filters;
using Principal.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class TeamController : Controller
    {
        private RepairEverythingEntities assistant = new RepairEverythingEntities();

        [HttpGet]
        [UserAuthorize(operationId: 9)]
        public ActionResult TeamList()
        {
            return View(assistant.Teams.Include(c => c.Client).ToList());
        }

        [HttpGet]
        [ActionName("Details")]
        [UserAuthorize(operationId: 9)]
        public PartialViewResult Details(int? id)
        {
            return PartialView("_Details", assistant.Teams.Include(t => t.Client).Where(t => t.id == id).FirstOrDefault());
        }

        [HttpGet]
        [ActionName("NewTeam")]
        [UserAuthorize(operationId: 10)]
        public PartialViewResult NewTeam()
        {
            ViewBag.client_id = new SelectList(assistant.Clients.ToList(), "id", "name");
            return PartialView("_NewTeam");
        }

        [HttpPost]
        [ActionName("NewTeam")]
        [UserAuthorize(operationId: 10)]
        public JsonResult NewTeam(Team team)
        {
            string result = "success";

            team.reception = DateTime.Now;
            team.url_image = "./../Images/Default.jpg";

            try
            {
                assistant.Teams.Add(team);
                assistant.SaveChanges();
            }
            catch (Exception)
            {
                result = "error";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("TeamEdit")]
        [UserAuthorize(operationId: 11)]
        public PartialViewResult TeamEdit(int? id)
        {
            var team = assistant.Teams.Include(t => t.Client).Where(t => t.id == id).FirstOrDefault();

            ViewBag.client_id = new SelectList(assistant.Clients.ToList(), "id", "name", team.client_id);
            return PartialView("_TeamEdit", team);
        }

        [HttpPost]
        [ActionName("TeamEdit")]
        [UserAuthorize(operationId: 11)]
        public JsonResult TeamEdit(Team team)
        {
            string result = "success";

            try
            {
                assistant.Entry(team).State = EntityState.Modified;
                assistant.SaveChanges();
            }
            catch (Exception)
            {
                result = "error";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("TeamDelete")]
        [UserAuthorize(operationId: 12)]
        public PartialViewResult TeamDelete(int? id)
        {
            return PartialView("_TeamDelete", assistant.Teams.Find(id));
        }

        [HttpPost]
        [ActionName("TeamDelete")]
        [UserAuthorize(operationId: 12)]
        public JsonResult TeamDelete(Team team)
        {
            string result = "success";

            try
            {
                assistant.Teams.Remove(assistant.Teams.Find(team.id));
                assistant.SaveChanges();
            }
            catch (Exception)
            {
                result = "error";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("TeamUpload")]
        public PartialViewResult TeamUpload(int? id)
        {
            return PartialView("_TeamUpload", assistant.Teams.Find(id));
        }

        [HttpPost]
        [ActionName("TeamUpload")]
        public JsonResult TeamUpload(HttpPostedFileBase file, int id)
        {
            string result = "success";
            
            try
            {
                string path = Server.MapPath("~/Images/");
                var team = assistant.Teams.Find(id);

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                file.SaveAs(path + System.IO.Path.GetFileName(file.FileName));
                team.url_image = "./../Images/" + file.FileName;

                assistant.Entry(team).State = EntityState.Modified;
                assistant.SaveChanges();
            }
            catch (Exception)
            {
                result = "error";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
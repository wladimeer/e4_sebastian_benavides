using Principal.Filters;
using Principal.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class WorkOrderController : Controller
    {
        private RepairEverythingEntities assistant = new RepairEverythingEntities();

        [HttpGet]
        [UserAuthorize(operationId: 5)]
        public ActionResult WorkOrderList()
        {
            if (Session["work_order_id"] != null)
            {
                Session.Remove("work_order_id");
            }

            return View(assistant.WorkOrders.Include(t => t.Team).Include(u => u.User).ToList());
        }

        [HttpGet]
        [UserAuthorize(operationId: 6)]
        public ActionResult NewWorkOrder()
        {
            ViewBag.team_id = new SelectList(assistant.Teams.ToList(), "id", "description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize(operationId: 6)]
        public ActionResult NewWorkOrder(WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = (User)Session["user"];

                    workOrder.date = DateTime.Now;
                    workOrder.user_id = user.id;

                    assistant.WorkOrders.Add(workOrder);
                    assistant.SaveChanges();
                }
                catch (Exception)
                {
                    TempData["error"] = "Hubo un Error al Agregar!";
                    return RedirectToAction("WorkOrderList", "WorkOrder");
                }

                TempData["success"] = "Orden de Trabajo Agregada Exitosamente!";
                return RedirectToAction("WorkOrderList", "WorkOrder");
            }

            ViewBag.team_id = new SelectList(assistant.Teams.ToList(), "id", "description", workOrder.team_id);
            return View(workOrder);
        }

        [HttpGet]
        [UserAuthorize(operationId: 7)]
        public ActionResult WorkOrderUpdate(int? id)
        {
            var t = assistant.WorkOrders.Find(id);

            ViewBag.team_id = new SelectList(assistant.Teams.ToList(), "id", "description", t.team_id);
            return View(assistant.WorkOrders.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize(operationId: 7)]
        public ActionResult WorkOrderUpdate(WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    assistant.Entry(workOrder).State = EntityState.Modified;
                    assistant.SaveChanges();
                }
                catch (Exception)
                {
                    TempData["error"] = "Hubo un Error al Actualizar!";
                    return RedirectToAction("WorkOrderList", "WorkOrder");
                }

                TempData["error"] = "Order de Trabajo Actualizada Exitosamente!";
                return RedirectToAction("WorkOrderList", "WorkOrder");
            }

            ViewBag.team_id = new SelectList(assistant.Teams.ToList(), "id", "description", workOrder.team_id);
            return View(assistant.WorkOrders.Find(workOrder.id));
        }

        [HttpGet]
        [UserAuthorize(operationId: 8)]
        public ActionResult WorkOrderDelete(int? id)
        {
            return View(assistant.WorkOrders.Include(t => t.Team).Where(w => w.id == id).FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize(operationId: 8)]
        public ActionResult WorkOrderDelete(WorkOrder workOrder)
        {
            try
            {
                assistant.WorkOrders.Remove(assistant.WorkOrders.Find(workOrder.id));
                assistant.SaveChanges();
            }
            catch (Exception)
            {
                TempData["error"] = "Hubo un Error al Eliminar!";
                return RedirectToAction("WorkOrderList", "WorkOrder");
            }

            TempData["error"] = "Orden de Trabajo Eliminada Exitosamente!";
            return RedirectToAction("WorkOrderList", "WorkOrder");
        }
    }
}
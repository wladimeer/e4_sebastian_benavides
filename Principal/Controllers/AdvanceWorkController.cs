using Principal.Filters;
using Principal.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class AdvanceWorkController : Controller
    {
        private RepairEverythingEntities assistant = new RepairEverythingEntities();

        [HttpGet]
        [UserAuthorize(5)]
        public ActionResult AdvanceWorkList(int? id)
        {
            Session["work_order_id"] = id;
            return View(assistant.AdvanceWorkOrders.Where(w => w.work_order_id == id).ToList());
        }

        [HttpGet]
        [UserAuthorize(6)]
        public ActionResult NewAdvanceWork()
        {
            return View();
        }

        [HttpPost]
        [UserAuthorize(6)]
        [ValidateAntiForgeryToken]
        public ActionResult NewAdvanceWork(AdvanceWorkOrder advanceWorkOrder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var work_order_id = (int)Session["work_order_id"];
                    advanceWorkOrder.work_order_id = work_order_id;

                    assistant.AdvanceWorkOrders.Add(advanceWorkOrder);
                    assistant.SaveChanges();
                }
                catch (Exception)
                {
                    TempData["error"] = "Hubo un Error al Agregar!";
                    return RedirectToAction("AdvanceWorkList", "AdvanceWork", new { id = advanceWorkOrder.work_order_id });
                }

                TempData["success"] = "Avance de Trabajo Agregado Exitosamente!";
                return RedirectToAction("AdvanceWorkList", "AdvanceWork", new { id = advanceWorkOrder.work_order_id });
            }

            return View(advanceWorkOrder);
        }

        [HttpGet]
        [UserAuthorize(7)]
        public ActionResult AdvanceWorkUpdate(int? id)
        {
            return View(assistant.AdvanceWorkOrders.Find(id));
        }

        [HttpPost]
        [UserAuthorize(7)]
        [ValidateAntiForgeryToken]
        public ActionResult AdvanceWorkUpdate(AdvanceWorkOrder advanceWorkOrder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    assistant.Entry(advanceWorkOrder).State = EntityState.Modified;
                    assistant.SaveChanges();
                }
                catch (Exception)
                {
                    TempData["error"] = "Hubo un Error al Actualizar!";
                    return RedirectToAction("AdvanceWorkList", "AdvanceWork", new { id = advanceWorkOrder.work_order_id });
                }

                TempData["success"] = "Avance de Trabajo Actualizado Exitosamente!";
                return RedirectToAction("AdvanceWorkList", "AdvanceWork", new { id = advanceWorkOrder.work_order_id });
            }

            return View(assistant.AdvanceWorkOrders.Find(advanceWorkOrder.id));
        }

        [HttpGet]
        [UserAuthorize(8)]
        public ActionResult AdvanceWorkDelete(int? id)
        {
            return View(assistant.AdvanceWorkOrders.Find(id));
        }

        [HttpPost]
        [UserAuthorize(8)]
        [ValidateAntiForgeryToken]
        public ActionResult AdvanceWorkDelete(AdvanceWorkOrder advanceWorkOrder)
        {
            int work_order_id = advanceWorkOrder.work_order_id;

            try
            {
                assistant.AdvanceWorkOrders.Remove(assistant.AdvanceWorkOrders.Find(advanceWorkOrder.id));
                assistant.SaveChanges();
            }
            catch (Exception)
            {
                TempData["error"] = "Hubo un Error al Eliminar!";
                return RedirectToAction("AdvanceWorkList", "AdvanceWork", new { id = advanceWorkOrder.work_order_id });
            }

            return RedirectToAction("AdvanceWorkList", "AdvanceWork", new { id = work_order_id });
        }
    }
}
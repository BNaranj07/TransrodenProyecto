using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TransrodenProyecto.Models;

namespace TransrodenProyecto.Controllers
{
    public class HistorialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Historials
        public ActionResult Index()
        {
            var historiales = db.Historiales.Include(h => h.Paquete);
            return View(historiales.ToList());
        }

        // GET: Historials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historial historial = db.Historiales.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            return View(historial);
        }

        // GET: Historials/Create
        public ActionResult Create()
        {
            ViewBag.Id_Paquete = new SelectList(db.Paquetes, "Id_Paquete", "NumeroRastreo");
            return View();
        }

        // POST: Historials/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Historial,Id_Paquete,Estado,Fecha")] Historial historial)
        {
            if (ModelState.IsValid)
            {
                db.Historiales.Add(historial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Paquete = new SelectList(db.Paquetes, "Id_Paquete", "NumeroRastreo", historial.Id_Paquete);
            return View(historial);
        }

        // GET: Historials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historial historial = db.Historiales.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Paquete = new SelectList(db.Paquetes, "Id_Paquete", "NumeroRastreo", historial.Id_Paquete);
            return View(historial);
        }

        // POST: Historials/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Historial,Id_Paquete,Estado,Fecha")] Historial historial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Paquete = new SelectList(db.Paquetes, "Id_Paquete", "NumeroRastreo", historial.Id_Paquete);
            return View(historial);
        }

        // GET: Historials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historial historial = db.Historiales.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            return View(historial);
        }

        // POST: Historials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Historial historial = db.Historiales.Find(id);
            db.Historiales.Remove(historial);
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

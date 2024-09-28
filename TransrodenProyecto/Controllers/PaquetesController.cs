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
    public class PaquetesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paquetes
        public ActionResult Index()
        {
            var paquetes = db.Paquetes.Include(p => p.Carga).Include(p => p.Envio);
            return View(paquetes.ToList());
        }

        // GET: Paquetes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paquete paquete = db.Paquetes.Find(id);
            if (paquete == null)
            {
                return HttpNotFound();
            }
            return View(paquete);
        }

        // GET: Paquetes/Create
        public ActionResult Create()
        {
            ViewBag.Id_Carga = new SelectList(db.Cargas, "Id_Carga", "Id_Carga");
            ViewBag.Id_Envio = new SelectList(db.Envios, "Id_Envio", "Id_Envio");
            return View();
        }

        // POST: Paquetes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Paquete,NumeroRastreo,Tipo,Emisor,Receptor,Direccion,Pago,Descripcion,Id_Carga,Id_Envio,fecha_entrega")] Paquete paquete)
        {
            if (ModelState.IsValid)
            {
                db.Paquetes.Add(paquete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Carga = new SelectList(db.Cargas, "Id_Carga", "Id_Carga", paquete.Id_Carga);
            ViewBag.Id_Envio = new SelectList(db.Envios, "Id_Envio", "Id_Envio", paquete.Id_Envio);
            return View(paquete);
        }

        // GET: Paquetes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paquete paquete = db.Paquetes.Find(id);
            if (paquete == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Carga = new SelectList(db.Cargas, "Id_Carga", "Id_Carga", paquete.Id_Carga);
            ViewBag.Id_Envio = new SelectList(db.Envios, "Id_Envio", "Id_Envio", paquete.Id_Envio);
            return View(paquete);
        }

        // POST: Paquetes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Paquete,NumeroRastreo,Tipo,Emisor,Receptor,Direccion,Pago,Descripcion,Id_Carga,Id_Envio,fecha_entrega")] Paquete paquete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paquete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Carga = new SelectList(db.Cargas, "Id_Carga", "Id_Carga", paquete.Id_Carga);
            ViewBag.Id_Envio = new SelectList(db.Envios, "Id_Envio", "Id_Envio", paquete.Id_Envio);
            return View(paquete);
        }

        // GET: Paquetes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paquete paquete = db.Paquetes.Find(id);
            if (paquete == null)
            {
                return HttpNotFound();
            }
            return View(paquete);
        }

        // POST: Paquetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paquete paquete = db.Paquetes.Find(id);
            db.Paquetes.Remove(paquete);
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

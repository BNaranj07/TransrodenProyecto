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
    public class FacturacionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Facturacions
        public ActionResult Index()
        {
            var facturaciones = db.Facturaciones.Include(f => f.Paquete).Include(f => f.Usuario);
            return View(facturaciones.ToList());
        }

        // GET: Facturacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturaciones.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            return View(facturacion);
        }

        // GET: Facturacions/Create
        public ActionResult Create()
        {
            ViewBag.Id_Paquete = new SelectList(db.Paquetes, "Id_Paquete", "NumeroRastreo");
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre");
            return View();
        }

        // POST: Facturacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Facturacion,Id_Paquete,Id_Usuario,Precio,Subtotal,Total,Fecha")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                db.Facturaciones.Add(facturacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Paquete = new SelectList(db.Paquetes, "Id_Paquete", "NumeroRastreo", facturacion.Id_Paquete);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre", facturacion.Id_Usuario);
            return View(facturacion);
        }

        // GET: Facturacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturaciones.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Paquete = new SelectList(db.Paquetes, "Id_Paquete", "NumeroRastreo", facturacion.Id_Paquete);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre", facturacion.Id_Usuario);
            return View(facturacion);
        }

        // POST: Facturacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Facturacion,Id_Paquete,Id_Usuario,Precio,Subtotal,Total,Fecha")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Paquete = new SelectList(db.Paquetes, "Id_Paquete", "NumeroRastreo", facturacion.Id_Paquete);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre", facturacion.Id_Usuario);
            return View(facturacion);
        }

        // GET: Facturacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturaciones.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            return View(facturacion);
        }

        // POST: Facturacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facturacion facturacion = db.Facturaciones.Find(id);
            db.Facturaciones.Remove(facturacion);
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

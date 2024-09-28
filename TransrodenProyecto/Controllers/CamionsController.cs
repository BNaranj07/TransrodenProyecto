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
    public class CamionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Camions
        public ActionResult Index()
        {
            var camiones = db.Camiones.Include(c => c.Usuario);
            return View(camiones.ToList());
        }

        // GET: Camions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camion camion = db.Camiones.Find(id);
            if (camion == null)
            {
                return HttpNotFound();
            }
            return View(camion);
        }

        // GET: Camions/Create
        public ActionResult Create()
        {
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre");
            return View();
        }

        // POST: Camions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Camion,Id_Usuario,Marca,Modelo,Tipo,Disponible")] Camion camion)
        {
            if (ModelState.IsValid)
            {
                db.Camiones.Add(camion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre", camion.Id_Usuario);
            return View(camion);
        }

        // GET: Camions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camion camion = db.Camiones.Find(id);
            if (camion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre", camion.Id_Usuario);
            return View(camion);
        }

        // POST: Camions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Camion,Id_Usuario,Marca,Modelo,Tipo,Disponible")] Camion camion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(camion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre", camion.Id_Usuario);
            return View(camion);
        }

        // GET: Camions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camion camion = db.Camiones.Find(id);
            if (camion == null)
            {
                return HttpNotFound();
            }
            return View(camion);
        }

        // POST: Camions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Camion camion = db.Camiones.Find(id);
            db.Camiones.Remove(camion);
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

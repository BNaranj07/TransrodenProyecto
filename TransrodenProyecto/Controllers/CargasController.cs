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
    public class CargasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cargas
        public ActionResult Index()
        {
            var cargas = db.Cargas.Include(c => c.Usuario);
            return View(cargas.ToList());
        }

        // GET: Cargas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carga carga = db.Cargas.Find(id);
            if (carga == null)
            {
                return HttpNotFound();
            }
            return View(carga);
        }

        // GET: Cargas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre");
            return View();
        }

        // POST: Cargas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Carga,Id_Usuario,Estado")] Carga carga)
        {
            if (ModelState.IsValid)
            {
                db.Cargas.Add(carga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre", carga.Id_Usuario);
            return View(carga);
        }

        // GET: Cargas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carga carga = db.Cargas.Find(id);
            if (carga == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre", carga.Id_Usuario);
            return View(carga);
        }

        // POST: Cargas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Carga,Id_Usuario,Estado")] Carga carga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre", carga.Id_Usuario);
            return View(carga);
        }

        // GET: Cargas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carga carga = db.Cargas.Find(id);
            if (carga == null)
            {
                return HttpNotFound();
            }
            return View(carga);
        }

        // POST: Cargas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carga carga = db.Cargas.Find(id);
            db.Cargas.Remove(carga);
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

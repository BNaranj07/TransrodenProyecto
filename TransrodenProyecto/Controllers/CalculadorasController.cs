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
    public class CalculadorasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Calculadoras
        public ActionResult Index()
        {
            return View(db.Calculadoras.ToList());
        }

        // GET: Calculadoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculadora calculadora = db.Calculadoras.Find(id);
            if (calculadora == null)
            {
                return HttpNotFound();
            }
            return View(calculadora);
        }

        // GET: Calculadoras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calculadoras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Calc,Tipo,Tarifa")] Calculadora calculadora)
        {
            if (ModelState.IsValid)
            {
                db.Calculadoras.Add(calculadora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(calculadora);
        }

        // GET: Calculadoras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculadora calculadora = db.Calculadoras.Find(id);
            if (calculadora == null)
            {
                return HttpNotFound();
            }
            return View(calculadora);
        }

        // POST: Calculadoras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Calc,Tipo,Tarifa")] Calculadora calculadora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calculadora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calculadora);
        }

        // GET: Calculadoras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculadora calculadora = db.Calculadoras.Find(id);
            if (calculadora == null)
            {
                return HttpNotFound();
            }
            return View(calculadora);
        }

        // POST: Calculadoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calculadora calculadora = db.Calculadoras.Find(id);
            db.Calculadoras.Remove(calculadora);
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

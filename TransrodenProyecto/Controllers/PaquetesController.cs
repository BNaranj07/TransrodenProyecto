using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TransrodenProyecto.Models;

namespace TransrodenProyecto.Controllers
{
    public class PaquetesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paquetes
        public ActionResult Index(string searchString, string searchName, string searchCedula, DateTime? startDate, DateTime? endDate)
        {
            var paquetes = from p in db.Paquetes select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                paquetes = paquetes.Where(p => p.NumeroRastreo.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchName))
            {
                paquetes = paquetes.Where(p => p.NombreEmisor.Contains(searchName) || p.NombreReceptor.Contains(searchName));
            }

            if (!String.IsNullOrEmpty(searchCedula))
            {
                paquetes = paquetes.Where(p => p.CedulaEmisor.Contains(searchCedula) || p.CedulaReceptor.Contains(searchCedula));
            }

            if (startDate.HasValue)
            {
                paquetes = paquetes.Where(p => p.fecha_recibo >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                paquetes = paquetes.Where(p => p.fecha_recibo <= endDate.Value);
            }

            if (!paquetes.Any())
            {
                ViewBag.NoResults = "No se encontraron resultados.";
            }

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

        // GET: Paquetes/RegistrarPaquete
        public ActionResult RegistrarPaquete()
        {
            return View();
        }

        // Crear y guardar el paquete
        [HttpPost]
        public async Task<ActionResult> RegistrarPaquete(Paquete model)
        {
            if (ModelState.IsValid)
            {
                // Obtener la sede del usuario desde la sesión
                int? sedeValue = Session["Sede"] as int?;
                EstadoPaquete estado; // Cambiar a EstadoPaquete

                if (sedeValue.HasValue)
                {
                    // Dependiendo de la sede, asignar el estado correspondiente
                    estado = sedeValue.Value == (int)Sede.SanJose ? EstadoPaquete.SinAsignarSJ : EstadoPaquete.SinAsignarPZ;
                }
                else
                {
                    estado = EstadoPaquete.SinAsignar; // Valor por defecto si no hay sede
                }
                var nuevoPaquete = new Paquete
                {
                    NumeroRastreo = GenerarNumeroRastreo(),
                    Tipo = model.Tipo,
                    Estado = estado,
                    NombreEmisor = model.NombreEmisor,
                    CedulaEmisor = model.CedulaEmisor,
                    NombreReceptor = model.NombreReceptor,
                    CedulaReceptor = model.CedulaReceptor,
                    Domicilio = model.Domicilio,
                    Direccion = model.Direccion,
                    TelefonoDomicilio = model.TelefonoDomicilio,
                    Cantidad = model.Cantidad,
                    Pago = model.Pago,
                    Descripcion = model.Descripcion,
                    fecha_recibo = System.DateTime.Now
                };

                db.Paquetes.Add(nuevoPaquete);
                await db.SaveChangesAsync();

                // Redirije a Facturación
                return RedirectToAction("GenerarFactura", "Facturacions", new { idPaquete = nuevoPaquete.Id_Paquete });
            }

            return View(model);
        }

        // Numero de tracking
        private string GenerarNumeroRastreo()
        {
            return Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
        }

        // GET: Paquetes/Create
        public ActionResult Create()
        {
            ViewBag.Id_Carga = new SelectList(db.Cargas, "Id_Carga", "Id_Carga");
            ViewBag.Id_Envio = new SelectList(db.Envios, "Id_Envio", "Id_Envio");
            return View();
        }

        // POST: Paquetes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Paquete,NumeroRastreo,Tipo,Estado,NombreEmisor,CedulaEmisor,NombreReceptor,CedulaReceptor,Domicilio,Direccion,TelefonoDomicilio,Cantidad,Pago,Descripcion,Id_Carga,Id_Envio,fecha_recibo,fecha_entrega")] Paquete paquete)
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
        /* Solucion real
        // POST: Paquetes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Paquete,NumeroRastreo,Tipo,NombreEmisor,CedulaEmisor,NombreReceptor,CedulaReceptor,Estado,Domicilio,Direccion,TelefonoDomicilio,Cantidad,Pago,Descripcion,Id_Carga,Id_Envio,fecha_recibo,fecha_entrega")] Paquete paquete)
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
        */

        // POST: Paquetes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_Paquete,NumeroRastreo,Tipo,NombreEmisor,CedulaEmisor,NombreReceptor,CedulaReceptor,Domicilio,Direccion,TelefonoDomicilio,Cantidad,Pago,Descripcion,Id_Carga,Id_Envio,fecha_recibo,fecha_entrega,Estado")] Paquete paquete)
        {
            if (ModelState.IsValid)
            {
                // Buscar el paquete existente
                var paqueteExistente = db.Paquetes.Find(paquete.Id_Paquete);

                if (paqueteExistente != null)
                {
                    // Actualizar los detalles del paquete
                    paqueteExistente.NumeroRastreo = paquete.NumeroRastreo;
                    paqueteExistente.Tipo = paquete.Tipo;
                    paqueteExistente.NombreEmisor = paquete.NombreEmisor;
                    paqueteExistente.CedulaEmisor = paquete.CedulaEmisor;
                    paqueteExistente.NombreReceptor = paquete.NombreReceptor;
                    paqueteExistente.CedulaReceptor = paquete.CedulaReceptor;
                    paqueteExistente.Domicilio = paquete.Domicilio;
                    paqueteExistente.Direccion = paquete.Direccion;
                    paqueteExistente.TelefonoDomicilio = paquete.TelefonoDomicilio;
                    paqueteExistente.Cantidad = paquete.Cantidad;
                    paqueteExistente.Pago = paquete.Pago;
                    paqueteExistente.Descripcion = paquete.Descripcion;
                    paqueteExistente.fecha_recibo = paquete.fecha_recibo;
                    paqueteExistente.fecha_entrega = paquete.fecha_entrega;
                    paqueteExistente.Estado = paquete.Estado;

                    // Guardar los cambios en el paquete
                    db.Entry(paqueteExistente).State = EntityState.Modified;

                    // Buscar la facturación asociada usando el Id_Paquete
                    var facturacionExistente = await db.Facturaciones.FirstOrDefaultAsync(f => f.Id_Paquete == paquete.Id_Paquete);

                    // Si existe una factura, eliminarla
                    if (facturacionExistente != null)
                    {
                        db.Facturaciones.Remove(facturacionExistente);
                        await db.SaveChangesAsync();
                    }

                    // Guardar cambios en el paquete después de eliminar la factura
                    await db.SaveChangesAsync();

                    // Redirigir a la acción de generar una nueva factura
                    return RedirectToAction("GenerarFactura", "Facturacions", new { idPaquete = paquete.Id_Paquete });
                }
            }

            // Si la validación falla o el paquete no existe, volver a mostrar el formulario
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

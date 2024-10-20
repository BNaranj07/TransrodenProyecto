using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransrodenProyecto.Models;
using TransrodenProyecto.ViewModels;


namespace TransrodenProyecto.Controllers
{
    public class PaquetesCargasControllerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AsignarPaqueteSJCarga()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                Paquetes = db.Paquetes.Where(p => p.Estado == EstadoPaquete.SinAsignarSJ).ToList(),
                Cargas = db.Cargas.ToList()
            };

            return View(viewModel);
        }

        public ActionResult AsignarPaquetePZCarga()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                Paquetes = db.Paquetes.Where(p => p.Estado == EstadoPaquete.SinAsignarPZ).ToList(),
                Cargas = db.Cargas.ToList()
            };

            return View(viewModel);
        }


    }
}

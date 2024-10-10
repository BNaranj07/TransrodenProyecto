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

        public ActionResult AsignarPaqueteCarga()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                Paquetes = db.Paquetes.Where(p => p.Estado == EstadoPaquete.SinAsignar).ToList(),
                Cargas = db.Cargas.ToList()
            };

            return View(viewModel);
        }

    }
}

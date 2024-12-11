using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TransrodenProyecto.Models;
using TransrodenProyecto.ViewModels;


namespace TransrodenProyecto.Controllers
{
    public class PaquetesCargasControllerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // ++++++++++++++++++++++++++++++++++++++++++ Vista del modulo de tracking ++++++++++++++++++++++++++++++++++

        //Menu para todo relacionado a Tracking SJ
        public ActionResult DashboardSJ()
        {
            return View();
        }


        //Menu para todo relacionado a Tracking PZ
        public ActionResult DashboardPZ()
        {
            return View();
        }


        //Menu para la bodega de SJ
        public ActionResult BodegaSJ()
        {
            return View();
        }


        //Menu para la bodega de PZ
        public ActionResult BodegaPZ()
        {
            return View();
        }


        //Asignar paquetes a las cargas desde SJ
        public ActionResult AsignarPaqueteSJCarga()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                Paquetes = db.Paquetes.Where(p => p.Estado == EstadoPaquete.SinAsignarSJ && p.Origen == OrigenPaquete.SanJose).ToList(),
                Cargas = db.Cargas.Include(c => c.Usuario).Where(c => c.Estado == EstadoCarga.BodegaSJ && c.Origen == OrigenCarga.SanJose).ToList(),
                CargasRecibidas = db.Cargas.Include(c => c.Usuario).Where(c => c.Estado == EstadoCarga.BodegaSJ && c.Origen == OrigenCarga.PerezZeledon).ToList()
            };

            return View(viewModel);
        }


        //Asignar paquetes a las cargas desde PZ
        public ActionResult AsignarPaquetePZCarga()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                Paquetes = db.Paquetes.Where(p => p.Estado == EstadoPaquete.SinAsignarPZ && p.Origen == OrigenPaquete.PerezZeledon).ToList(),
                Cargas = db.Cargas.Include(c => c.Usuario).Where(c => c.Estado == EstadoCarga.BodegaPZ && c.Origen == OrigenCarga.PerezZeledon).ToList(),
                CargasRecibidas = db.Cargas.Include(c => c.Usuario).Where(c => c.Estado == EstadoCarga.BodegaPZ && c.Origen == OrigenCarga.SanJose).ToList()
            };

            return View(viewModel);
        }


        //Ver cargas en transito
        public ActionResult VistaCargaTransito()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                Cargas = db.Cargas.Include(c => c.Usuario).Where(c => c.Estado == EstadoCarga.EnTransito).ToList(),
                Envios = db.Envios.Include(c => c.Usuario).Where(c => c.Estado == EstadoEnvio.EnTransito).ToList()
            };

            return View(viewModel);
        }



        //Todos los paquetes que se encuentra en la bodega SJ
        public ActionResult PaquetesBodegaSJ()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                //PAQUETES QUE VIENEN SINASIGNAR, PAQUETES ASIGNADOS Y PAQUETES DE ORIGEN PZ, PAQUETES QUE NO FUERON ENTREGADOS
                Paquetes = db.Paquetes.Where(p => p.Estado == EstadoPaquete.SinAsignarSJ && p.Origen == OrigenPaquete.SanJose || 
                p.Estado == EstadoPaquete.BodegaSJ && p.Origen == OrigenPaquete.SanJose ||
                p.Estado == EstadoPaquete.BodegaSJ && p.Origen == OrigenPaquete.PerezZeledon && p.Carga.Estado == EstadoCarga.Recibido ||
                p.Estado == EstadoPaquete.NoEntregado && p.Origen == OrigenPaquete.PerezZeledon && p.Carga.Estado == EstadoCarga.Recibido && p.Envio.Estado == EstadoEnvio.Entregado).ToList(),

            };

            return View(viewModel);
        }



        //Todos los paquetes que se encuentra en la bodega SJ que no son domicilio
        public ActionResult PaquetesReclamoSJ()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                //PAQUETES ORIGEN PZ SIN DOMICILIO
                Paquetes = db.Paquetes.Where(p => p.Estado == EstadoPaquete.BodegaSJ && p.Origen == OrigenPaquete.PerezZeledon && p.Domicilio == false && p.Carga.Estado == EstadoCarga.Recibido).ToList(),
            };

            return View(viewModel);
        }





        //Todos los paquetes que se encuentra en la bodega PZ
        public ActionResult PaquetesBodegaPZ()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                //PAQUETES QUE VIENEN SINASIGNAR, PAQUETES ASIGNADOS Y PAQUETES DE ORIGEN SJ
                Paquetes = db.Paquetes.Where(p => p.Estado == EstadoPaquete.SinAsignarPZ && p.Origen == OrigenPaquete.PerezZeledon ||
                p.Estado == EstadoPaquete.BodegaPZ && p.Origen == OrigenPaquete.PerezZeledon ||
                p.Estado == EstadoPaquete.BodegaPZ && p.Origen == OrigenPaquete.SanJose && p.Carga.Estado == EstadoCarga.Recibido ||
                p.Estado == EstadoPaquete.NoEntregado && p.Origen == OrigenPaquete.SanJose && p.Carga.Estado == EstadoCarga.Recibido && p.Envio.Estado == EstadoEnvio.Entregado).ToList(),

            };

            return View(viewModel);
        }




        //Todos los paquetes que se encuentra en la bodega SJ que no son domicilio
        public ActionResult PaquetesReclamoPZ()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                //PAQUETES ORIGEN PZ SIN DOMICILIO
                Paquetes = db.Paquetes.Where(p => p.Estado == EstadoPaquete.BodegaPZ && p.Origen == OrigenPaquete.SanJose && p.Domicilio == false && p.Carga.Estado == EstadoCarga.Recibido).ToList(),
            };

            return View(viewModel);
        }



        public ActionResult PaquetesRechazoSJ()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                //PAQUETES QUE VIENEN SINASIGNAR, PAQUETES ASIGNADOS Y PAQUETES DE ORIGEN SJ
                Paquetes = db.Paquetes.Where(p => 
                p.Estado == EstadoPaquete.NoEntregado && p.Origen == OrigenPaquete.PerezZeledon && p.Carga.Estado == EstadoCarga.Recibido && p.Envio.Estado == EstadoEnvio.Entregado).ToList(),

            };

            return View(viewModel);
        }



        public ActionResult PaquetesRechazoPZ()
        {
            var viewModel = new PaqueteCargaViewModel
            {
                //PAQUETES QUE VIENEN SINASIGNAR, PAQUETES ASIGNADOS Y PAQUETES DE ORIGEN SJ
                Paquetes = db.Paquetes.Include(p => p.Envio).Where(p => 
                p.Estado == EstadoPaquete.NoEntregado && p.Origen == OrigenPaquete.SanJose && p.Carga.Estado == EstadoCarga.Recibido && p.Envio.Estado == EstadoEnvio.Entregado).ToList(),

            };

            return View(viewModel);
        }




        // ++++++++++++++++++++++++++++++++++++++++++ Metodo del modulo de tracking ++++++++++++++++++++++++++++++++++


        // Metodo para asignar los paquetes a las cargas SJ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarPaqueteACargaSJ(List<PaqueteCargaAsignacionViewModel> paqueteCargaAsignaciones)
        {
            if (paqueteCargaAsignaciones == null || !paqueteCargaAsignaciones.Any())
            {
                ModelState.AddModelError("", "No se han seleccionado cargas o paquetes");
                return RedirectToAction("AsignarPaqueteSJCarga");
            }

            foreach (var asignacion in paqueteCargaAsignaciones)
            {

                if (asignacion.IdCarga > 0)
                {
                    // Verifica si la carga existe
                    var carga = db.Cargas.FirstOrDefault(c => c.Id_Carga == asignacion.IdCarga && c.Estado == EstadoCarga.BodegaSJ);
                    if (carga == null)
                    {
                        continue; // Continuar si la carga no es valida
                    }

                    // Obtiene el paquete que se va asignar
                    var paquete = db.Paquetes.FirstOrDefault(p => p.Id_Paquete == asignacion.IdPaquete && p.Estado == EstadoPaquete.SinAsignarSJ);
                    if (paquete == null)
                    {
                        continue;
                    }


                    paquete.Id_Carga = carga.Id_Carga;
                    paquete.Estado = EstadoPaquete.BodegaSJ;

                    carga.NumeroPaquetes = (carga.NumeroPaquetes ?? 0) + 1;
                }
            }


            db.SaveChanges();
            return RedirectToAction("AsignarPaqueteSJCarga");
        }



        // Metodo para asignar los paquetes a las cargas PZ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarPaqueteACargaPZ(List<PaqueteCargaAsignacionViewModel> paqueteCargaAsignaciones)
        {
            if (paqueteCargaAsignaciones == null || !paqueteCargaAsignaciones.Any())
            {
                ModelState.AddModelError("", "No se han seleccionado cargas o paquetes");
                return RedirectToAction("AsignarPaquetePZCarga");
            }

            foreach (var asignacion in paqueteCargaAsignaciones)
            {

                if (asignacion.IdCarga > 0)
                {

                    var carga = db.Cargas.FirstOrDefault(c => c.Id_Carga == asignacion.IdCarga && c.Estado == EstadoCarga.BodegaPZ);
                    if (carga == null)
                    {
                        continue;
                    }

                    var paquete = db.Paquetes.FirstOrDefault(p => p.Id_Paquete == asignacion.IdPaquete && p.Estado == EstadoPaquete.SinAsignarPZ);
                    if (paquete == null)
                    {
                        continue;
                    }

                    paquete.Id_Carga = carga.Id_Carga;
                    paquete.Estado = EstadoPaquete.BodegaPZ;

                    carga.NumeroPaquetes = (carga.NumeroPaquetes ?? 0) + 1;
                }
            }


            db.SaveChanges();
            return RedirectToAction("AsignarPaquetePZCarga");
        }



        // Metodo para quitar los paquetes de una carga
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuitarPaquete(int idPaquete, int idCarga)
        {

            //Se busca primero el paquete
            var paquete = db.Paquetes.FirstOrDefault(p => p.Id_Paquete == idPaquete && p.Id_Carga == idCarga);
            var carga = db.Cargas.FirstOrDefault(c => c.Id_Carga.Equals(idCarga));


            if (paquete == null)
            {
                return HttpNotFound("El paquete no fue encontrado o no pertenece a la carga especificada.");
            }


            // Aqui vuelve a pasar a nulo (estado original del campo)
            paquete.Id_Carga = null;


            // Poner el estado original del paquete
            if (paquete.Origen == OrigenPaquete.SanJose)
            {
                paquete.Estado = EstadoPaquete.SinAsignarSJ;
            }
            else if (paquete.Origen == OrigenPaquete.PerezZeledon)
            {
                paquete.Estado = EstadoPaquete.SinAsignarPZ;
            }
            else
            {
                paquete.Estado = EstadoPaquete.SinAsignar;
            }

            carga.NumeroPaquetes = (carga.NumeroPaquetes ?? 0) - 1;

            db.SaveChanges();

            return RedirectToAction("CargaPaquetes", new { idCarga = idCarga });
        }





        // Muestra los paquetes que estan asignados a la carga pero para las vistas de asignacion
        public ActionResult CargaPaquetes(int idCarga)
        {
            var carga = db.Cargas.Include(c => c.Usuario).FirstOrDefault(c => c.Id_Carga == idCarga);

            if (carga == null)
            {
                return HttpNotFound("Cargas no encontradas");
            }

            // Paquetes asociados a la carga
            var paquetes = db.Paquetes.Where(p => p.Id_Carga == idCarga).ToList();


            // Este viewModel funciona para cargar la vista
            var viewModel = new PaqueteCargaViewModel
            {
                Cargas = new List<Carga> { carga },
                Paquetes = paquetes
            };

            return View(viewModel);
        }


        //Para otras vista donde solo se requiera ver el paquete nada mas
        public ActionResult CargaPaquetesView(int idCarga)
        {
            var carga = db.Cargas.Include(c => c.Usuario).FirstOrDefault(c => c.Id_Carga == idCarga);

            if (carga == null)
            {
                return HttpNotFound("Cargas no encontradas");
            }

            // Paquetes asociados a la carga
            var paquetes = db.Paquetes.Where(p => p.Id_Carga == idCarga).ToList();


            // Este viewModel funciona para cargar la vista
            var viewModel = new PaqueteCargaViewModel
            {
                Cargas = new List<Carga> { carga },
                Paquetes = paquetes
            };

            return View(viewModel);
        }


        // Vista para Transportistas donde solo se requiera ver el paquete nada mas

        public ActionResult CargaPaquetesViewTransp(int idCarga, string searchTerm = "", int page = 1)
        {
            var carga = db.Cargas.Include(c => c.Usuario).FirstOrDefault(c => c.Id_Carga == idCarga);

            if (carga == null)
            {
                return HttpNotFound("Cargas no encontradas");
            }

            // Paquetes asociados a la carga
            var paquetesQuery = db.Paquetes.Where(p => p.Id_Carga == idCarga);

            // Apply search filter if search term is provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                paquetesQuery = paquetesQuery.Where(p => p.NumeroRastreo.Contains(searchTerm));
            }

            // Pagination
            int pageSize = 10;
            int totalPaquetes = paquetesQuery.Count();
            int totalPages = (int)Math.Ceiling((double)totalPaquetes / pageSize);

            var paquetes = paquetesQuery
                .OrderBy(p => p.Id_Paquete)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Este viewModel funciona para cargar la vista
            var viewModel = new PaqueteCargaViewModel
            {
                Cargas = new List<Carga> { carga },
                Paquetes = paquetes
            };

            // Pasar datos de paginación a la vista mediante ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.IdCarga = idCarga;

            return View(viewModel);
        }






        // Actualizar el estado de la carga la cual tambien cambiara la de los paquetes SJ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarEstadoCargaSJ(int idCarga, string nuevoEstado)
        {

            if (string.IsNullOrEmpty(nuevoEstado))
            {
                ModelState.AddModelError("", "Seleccione un Estado!!");
                return RedirectToAction("AsignarPaqueteSJCarga");
            }


            var carga = db.Cargas.Include(c => c.Paquetes).FirstOrDefault(c => c.Id_Carga == idCarga);

            if (carga == null)
            {
                return HttpNotFound("Carga no encontrada!");
            }



            // TryParse convierte un string en un valor enum
            if (Enum.TryParse<EstadoCarga>(nuevoEstado, out var estadoResult))
            {
                carga.Estado = estadoResult;


                foreach (var paquete in carga.Paquetes)
                {
                    if (carga.Estado == EstadoCarga.BodegaSJ)
                    {
                        paquete.Estado = EstadoPaquete.BodegaSJ;
                    }
                    else if (carga.Estado == EstadoCarga.BodegaPZ)
                    {
                        paquete.Estado = EstadoPaquete.BodegaPZ;
                    }
                    else if (carga.Estado == EstadoCarga.EnTransito)
                    {
                        paquete.Estado = EstadoPaquete.EnTransito;
                    }
                    else if (carga.Estado == EstadoCarga.Entregado)
                    {
                        paquete.Estado = EstadoPaquete.Entregado;
                    }


                    var nuevoRastreo = new Historial
                    {
                        Id_Paquete = paquete.Id_Paquete,
                        Estado = paquete.Estado,
                        NumeroRastreo = paquete.NumeroRastreo, 
                        Fecha = DateTime.Now
                    };

                    db.Historiales.Add(nuevoRastreo);

                }

                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Estado invalido!!");
            }

            return RedirectToAction("AsignarPaqueteSJCarga");
        }



        // Actualizar el estado de la carga la cual tambien cambiara la de los paquetes PZ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarEstadoCargaPZ(int idCarga, string nuevoEstado)
        {

            if (string.IsNullOrEmpty(nuevoEstado))
            {
                ModelState.AddModelError("", "Seleccione un Estado!!");
                return RedirectToAction("AsignarPaquetePZCarga");
            }


            var carga = db.Cargas.Include(c => c.Paquetes).FirstOrDefault(c => c.Id_Carga == idCarga);

            if (carga == null)
            {
                return HttpNotFound("Carga no encontrada!");
            }



            // TryParse convierte un string en un valor enum
            if (Enum.TryParse<EstadoCarga>(nuevoEstado, out var estadoResult))
            {
                carga.Estado = estadoResult;


                foreach (var paquete in carga.Paquetes)
                {
                    if (carga.Estado == EstadoCarga.BodegaSJ)
                    {
                        paquete.Estado = EstadoPaquete.BodegaSJ;
                    }
                    else if (carga.Estado == EstadoCarga.BodegaPZ)
                    {
                        paquete.Estado = EstadoPaquete.BodegaPZ;
                    }
                    else if (carga.Estado == EstadoCarga.EnTransito)
                    {
                        paquete.Estado = EstadoPaquete.EnTransito;
                    }
                    else if (carga.Estado == EstadoCarga.Entregado)
                    {
                        paquete.Estado = EstadoPaquete.Entregado;
                    }

                    var nuevoRastreo = new Historial
                    {
                        Id_Paquete = paquete.Id_Paquete,
                        Estado = paquete.Estado,
                        NumeroRastreo = paquete.NumeroRastreo,
                        Fecha = DateTime.Now
                    };

                    db.Historiales.Add(nuevoRastreo);

                }

                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Estado invalido!!");
            }

            return RedirectToAction("AsignarPaquetePZCarga");
        }







        //Cambia el estado solo de la carga, esto es para las cargas que vienen de PZ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EstadoCargaSJ(int idCarga, string nuevoEstado)
        {

            if (string.IsNullOrEmpty(nuevoEstado))
            {
                ModelState.AddModelError("", "Seleccione un Estado!!");
                return RedirectToAction("AsignarPaqueteSJCarga");
            }


            var carga = db.Cargas.Include(c => c.Paquetes).FirstOrDefault(c => c.Id_Carga == idCarga);

            if (carga == null)
            {
                return HttpNotFound("Carga no encontrada!");
            }



            // TryParse convierte un string en un valor enum
            if (Enum.TryParse<EstadoCarga>(nuevoEstado, out var estadoResult))
            {
                carga.Estado = estadoResult;

                if (carga.Estado == EstadoCarga.Recibido)
                {
                    carga.fecha_entrega = DateTime.Now;
                }

                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Estado invalido!!");
            }

            return RedirectToAction("AsignarPaqueteSJCarga");
        }



        //Cambia el estado solo de la carga, esto es para las cargas que vienen de SJ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EstadoCargaPZ(int idCarga, string nuevoEstado)
        {

            if (string.IsNullOrEmpty(nuevoEstado))
            {
                ModelState.AddModelError("", "Seleccione un Estado!!");
                return RedirectToAction("AsignarPaquetePZCarga");
            }


            var carga = db.Cargas.Include(c => c.Paquetes).FirstOrDefault(c => c.Id_Carga == idCarga);

            if (carga == null)
            {
                return HttpNotFound("Carga no encontrada!");
            }

            // TryParse convierte un string en un valor enum
            if (Enum.TryParse<EstadoCarga>(nuevoEstado, out var estadoResult))
            {
                carga.Estado = estadoResult;

                if (carga.Estado == EstadoCarga.Recibido)
                {
                    carga.fecha_entrega = DateTime.Now;
                }

                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Estado invalido!!");
            }

            return RedirectToAction("AsignarPaquetePZCarga");
        }
 



        // SOlO PARA TRANSPORTISTA Y SOLO PARA REPORTE DE AVERIA // NO AFECTA PAQUETES Y NO CAMBIA EL ESTADO EN HISTORIAL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EstadoCargaTransp(int idCarga, string nuevoEstado)
        {

            if (string.IsNullOrEmpty(nuevoEstado))
            {
                ModelState.AddModelError("", "Seleccione un Estado!!");
                return Redirect("CargasTransportista");
            }


            var carga = db.Cargas.Include(c => c.Paquetes).FirstOrDefault(c => c.Id_Carga == idCarga);

            if (carga == null)
            {
                return HttpNotFound("Carga no encontrada!");
            }

            // TryParse convierte un string en un valor enum
            if (Enum.TryParse<EstadoCarga>(nuevoEstado, out var estadoResult))
            {
                carga.Estado = estadoResult;

                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Estado invalido!!");
            }

            return Redirect("CargasTransportista");
        }

        


        // Cambiar el estado de las cargas en la vista de cargas en transito
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarEstadoCargaGlobal(int idCarga, string nuevoEstado)
        {

            if (string.IsNullOrEmpty(nuevoEstado))
            {
                ModelState.AddModelError("", "Seleccione un Estado!!");
                return RedirectToAction("VistaCargaTransito");
            }


            var carga = db.Cargas.Include(c => c.Paquetes).FirstOrDefault(c => c.Id_Carga == idCarga);

            if (carga == null)
            {
                return HttpNotFound("Carga no encontrada!");
            }



            // TryParse convierte un string en un valor enum
            if (Enum.TryParse<EstadoCarga>(nuevoEstado, out var estadoResult))
            {
                carga.Estado = estadoResult;


                foreach (var paquete in carga.Paquetes)
                {
                    if (carga.Estado == EstadoCarga.BodegaSJ)
                    {
                        paquete.Estado = EstadoPaquete.BodegaSJ;
                    }
                    else if (carga.Estado == EstadoCarga.BodegaPZ)
                    {
                        paquete.Estado = EstadoPaquete.BodegaPZ;
                    }
                    else if (carga.Estado == EstadoCarga.EnTransito)
                    {
                        paquete.Estado = EstadoPaquete.EnTransito;
                    }
                    else if (carga.Estado == EstadoCarga.Entregado)
                    {
                        paquete.Estado = EstadoPaquete.Entregado;
                    }

                    var nuevoRastreo = new Historial
                    {
                        Id_Paquete = paquete.Id_Paquete,
                        Estado = paquete.Estado,
                        NumeroRastreo = paquete.NumeroRastreo,
                        Fecha = DateTime.Now
                    };

                    db.Historiales.Add(nuevoRastreo);

                }

                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Estado invalido!!");
            }

            return RedirectToAction("VistaCargaTransito");
        }


        // Cambia solo el estado del paquete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarEstadoPaquete(int idPaquete, string nuevoEstado)
        {
            if (string.IsNullOrEmpty(nuevoEstado))
            {
                TempData["Error"] = "Seleccione un estado válido.";
                return RedirectToAction("PaquetesBodegaSJ");
            }

            var paquete = db.Paquetes.Find(idPaquete);
            if (paquete == null)
            {
                TempData["Error"] = "Paquete no encontrado!!";
                return RedirectToAction("PaquetesBodegaSJ");
            }


            paquete.Estado = (EstadoPaquete)Enum.Parse(typeof(EstadoPaquete), nuevoEstado);
            
            if (paquete.Estado == EstadoPaquete.Entregado)
            {
                paquete.fecha_entrega = DateTime.Now;
            }


            var nuevoRastreo = new Historial
            {
                Id_Paquete = paquete.Id_Paquete,
                Estado = paquete.Estado,
                NumeroRastreo = paquete.NumeroRastreo,
                Fecha = DateTime.Now
            };

            db.Historiales.Add(nuevoRastreo);


            db.SaveChanges();


            TempData["Success"] = "El estado del paquete se ha actualizado correctamente.";
            //return RedirectToAction("PaquetesBodegaSJ");
            //Redirige a la vista desde donde fue accionado el metodo
            return Redirect(Request.UrlReferrer.ToString()); // ---------------------------------------------- < CAMBIAR > ----------------------------
        }



        // ++++++++++++++++++++++++++++++++++++++++++ Vista transportista del modulo de tracking ++++++++++++++++++++++++++++++++++

        
        // Muesta todos las cargas que tiene el usuario Transportista asignado
        public ActionResult CargasTransportista()
        {
            // Verificar si la sesión contiene la información del usuario
            if (Session["UsuarioId"] == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }

            // Obtener el usuario
            var usuarioId = (int)Session["UsuarioId"];
            var usuarioRol = (Rol)Session["UsuarioRol"];


            // Verificar si 'sede' tiene un valor antes de convertirlo
            var cargas = new List<Carga>();

            if (usuarioRol == Rol.Transportista)
            {
                //MUESTRA LAS CARGAS QUE SON PERTENECIENTES AL TRANSPORTISTA Y TENGAN ESTADO ENTRANSITO, BODEGASJ, BODEGAPZ
                cargas = db.Cargas.Include(c => c.Usuario).Where(c => c.Id_Usuario == usuarioId && c.Estado == EstadoCarga.EnTransito 
                    || c.Id_Usuario == usuarioId && c.Estado == EstadoCarga.BodegaSJ 
                    || c.Id_Usuario == usuarioId && c.Estado == EstadoCarga.BodegaPZ).ToList();
            }

            var viewModel = new PaqueteCargaViewModel
            {
                Cargas = cargas
            };

            return View(viewModel);
        }


        // Muesta todos las cargas entregadas que tiene el usuario Transportista asignado
        public ActionResult CargasEntregadasTransportista(string searchTerm = "", int page = 1)
        {
            // Verificar si la sesión contiene la información del usuario
            if (Session["UsuarioId"] == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }

            // Obtener el usuario
            var usuarioId = (int)Session["UsuarioId"];
            var usuarioRol = (Rol)Session["UsuarioRol"];

            // Verificar si 'sede' tiene un valor antes de convertirlo
            var cargasQuery = new List<Carga>();

            if (usuarioRol == Rol.Transportista)
            {
                //MUESTRA LAS CARGAS QUE SON PERTENECIENTES AL TRANSPORTISTA Y TENGAN ESTADO ENTREGADO O RECIBIDO
                cargasQuery = db.Cargas.Include(c => c.Usuario)
                    .Where(c => (c.Id_Usuario == usuarioId && (c.Estado == EstadoCarga.Entregado || c.Estado == EstadoCarga.Recibido)))
                    .ToList();
            }

            // Apply search filter if search term is provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                cargasQuery = cargasQuery.Where(c =>
                    c.Id_Carga.ToString().Contains(searchTerm) ||
                    c.Usuario.Nombre.Contains(searchTerm) ||
                    c.Origen.ToString().Contains(searchTerm)
                ).ToList();
            }

            // Pagination
            int pageSize = 10;
            int totalCargas = cargasQuery.Count();
            int totalPages = (int)Math.Ceiling((double)totalCargas / pageSize);

            var cargas = cargasQuery
                .OrderBy(c => c.Id_Carga)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new PaqueteCargaViewModel
            {
                Cargas = cargas
            };

            // Pasar datos de paginación a la vista mediante ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTerm = searchTerm;

            return View(viewModel);
        }

    }
}

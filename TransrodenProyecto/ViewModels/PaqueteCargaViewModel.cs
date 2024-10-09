using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransrodenProyecto.Models;

namespace TransrodenProyecto.ViewModels
{
    public class PaqueteCargaViewModel
    {
        public List<Paquete> Paquetes { get; set; }
        public List<Carga> Cargas { get; set; }
    }
}
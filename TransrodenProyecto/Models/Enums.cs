using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransrodenProyecto.Models
{
    public enum TipoPaquete
    {
        CajaPequeña = 0,
        CajaMediana = 1,
        CajaGrande = 2
    }

    public enum EstadoPaquete
    {
        SinAsignar = 0,
        Asignado = 1,
        EnTransito = 2,
        BodegaPZ = 3,
        BodegaSJ = 4,
        Domicilio = 5,
        Entregado = 6
    }

    public enum EstadoCarga
    {
        Asignado = 0,
        EnTransito = 1,
        BodegaPZ = 2,
        BodegaSJ = 3,
        Entregado = 4
    }

    public enum EstadoEnvio
    {
        Asignado = 0,
        EnTransito = 1,
        BodegaPZ = 2,
        BodegaSJ = 3,
        Entregado = 4
    }
    public enum Rol
    {
        Administrador = 0,
        Bodeguero = 1,
        Transportista = 2,
        Cliente = 3
    }

}
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
        SinAsignarSJ = 1,
        SinAsignarPZ = 2,
        Asignado = 3,
        EnTransito = 4,
        BodegaPZ = 5,
        BodegaSJ = 6,
        Domicilio = 7,
        Entregado = 8
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

    public enum Sede
    {
        SanJose = 0,
        PerezZeledon = 1
    }

}
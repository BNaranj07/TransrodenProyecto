using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransrodenProyecto.Models
{
    public class Paquete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Paquete { get; set; }


        [StringLength(50)]
        public string NumeroRastreo { get; set; } = string.Empty;


        //Enum
        [Required]
        public TipoPaquete Tipo { get; set; }

        //Enum
        public EstadoPaquete Estado { get; set; }

        public Paquete()
        {
            Estado = EstadoPaquete.SinAsignar; //Poner EstadoPaquete en valor 0 por defecto
        }



        [Required]
        public string NombreEmisor { get; set; }


        //Para Facturacion
        [Required]
        public string CedulaEmisor { get; set; }


        [Required]
        public string NombreReceptor { get; set; }


        // Para confirmacion de entrega 
        [Required]
        public string CedulaReceptor { get; set; }


        [Required]
        public bool Domicilio { get; set; }


        public string Direccion { get; set; }

        public string TelefonoDomicilio { get; set; }

        [Required]
        public string Cantidad { get; set; }

        [Required]
        public bool Pago { get; set; }


        public string Descripcion { get; set; }


        // +++++++++++++++++ Null ++++++++++++++++++++++++++++++
        // Se necesita de ? para que el campo acepte nulls
        // Tambien se necesita agregar la opcion virtual
        // Ademas agregar un modelBuilder.Entity con hasOptional
        public int? Id_Carga { get; set; }
        public virtual Carga Carga { get; set; }


        // +++++++++++++++++ Null ++++++++++++++++++++++++++++++
        // Se necesita de ? para que el campo acepte nulls
        // Tambien se necesita agregar la opcion virtual
        // Ademas agregar un modelBuilder.Entity con hasOptional
        public int? Id_Envio { get; set; }
        public virtual Envio Envio { get; set; }


        public DateTime fecha_recibo { get; set; }

        public DateTime? fecha_entrega { get; set; }


        public List<Historial> Historiales { get; set; } = new List<Historial>();

        public List<Facturacion> Facturaciones { get; set; } = new List<Facturacion>();

        public bool ConfirmarEliminacionFacturas { get; set; }


    }
}
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


        [Required]
        [StringLength(50)]
        public string NumeroRastreo { get; set; }


        //Enum
        [Required]
        public int Tipo { get; set; }


        [Required]
        public string Emisor { get; set; }


        [Required]
        public string Receptor { get; set; }


        [Required]
        public string Direccion { get; set; }


        [Required]
        public bool Pago { get; set; }


        public string Descripcion { get; set; }


        public int Id_Carga { get; set; }
        public Carga Carga { get; set; }


        public int Id_Envio { get; set; }
        public Envio Envio { get; set; }


        [Required]
        public DateTime fecha_entrega { get; set; }


        public List<Historial> Historiales { get; set; }

        public List<Facturacion> Facturaciones { get; set; }


    }
}
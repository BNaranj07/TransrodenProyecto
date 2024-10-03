using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransrodenProyecto.Models
{
    public class Facturacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Facturacion { get; set; }


        [Required]
        public int Id_Paquete { get; set; }
        public Paquete Paquete { get; set; }


        // Para que acepte null
        public int? Id_Usuario { get; set; }
        public Usuario Usuario { get; set; }


        [Required]
        public string NombreEmisor { get; set; }


        //Se utilizara para validar si el emisor tiene una cuenta en transroden y asi asociar el usuario con la factura (en caso de no tener cuenta, simplemente no se asocia a nada y se guarda la cedula)
        [Required]
        public string CedulaEmisor { get; set; }


        [Required]
        public double Precio { get; set; }


        [Required]
        public double Subtotal { get; set; }


        [Required]
        public double Total { get; set; }


        [Required]
        public DateTime Fecha { get; set; }
    }
}
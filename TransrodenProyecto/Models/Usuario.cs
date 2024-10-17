using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransrodenProyecto.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Usuario { get; set; }


        [Required]
        public string Nombre { get; set; }


        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string Cedula { get; set; }


        [Required]
        public string Correo { get; set; }


        [Required]
        public string Clave { get; set; }


        [Required]
        public string Telefono { get; set; }


        //Enum
        [Required]
        public Rol Rol { get; set; }


        public List<Facturacion> Facturaciones { get; set; }

        public List<Envio> Envios { get; set; }

        public List<Carga> Cargas { get; set; }

        public List<Camion> Camiones { get; set; }


        // Nuevas propiedades para manejo de bloqueo de usuario
        public int AccessFailedCount { get; set; } = 0;

        public bool LockoutEnabled { get; set; } = true;

        public DateTime? LockoutEnd { get; set; }


    }
}
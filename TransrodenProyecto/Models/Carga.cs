using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransrodenProyecto.Models
{
    public class Carga
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Carga { get; set; }



        public int Id_Usuario { get; set; }
        public Usuario Usuario { get; set; }


        //Enum
        [Required]
        public int Estado { get; set; }


        public List<Paquete> Paquetes { get; set; }
    }
}
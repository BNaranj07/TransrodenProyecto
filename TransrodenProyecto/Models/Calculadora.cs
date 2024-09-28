using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransrodenProyecto.Models
{
    public class Calculadora
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Calc { get; set; }


        // +++++++++  Los tipo de cajas disponible y sus tarifas

        //Enum
        [Required]
        public int Tipo { get; set; }


        [Required]
        public bool Tarifa { get; set; }

    }
}
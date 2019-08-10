using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Licencias
    {
        public int ID { get; set; }
        [Required]
        public int Empleado { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Desde { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Hasta { get; set; }
        [Required]
        [StringLength(50)]
        public string Motivo { get; set; }
        [StringLength(200)]
        public string Comentarios { get; set; }

        [ForeignKey("Empleado")]
        public Empleados Empleados { get; set; }
    }
}
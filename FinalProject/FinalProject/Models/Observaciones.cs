using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ProyectoFinal.Models
{
    public class Observaciones
    {
        public int ID { get; set; }
        [Required]
        public int Empleado { get; set; }
        [Required]
        [StringLength(100)]
        public string Observacion { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Fecha { get; set; }
        [StringLength(500)]
        public string Comentarios { get; set; }

        [ForeignKey("Empleado")]
        public Empleados Empleados { get; set; }
    }
}
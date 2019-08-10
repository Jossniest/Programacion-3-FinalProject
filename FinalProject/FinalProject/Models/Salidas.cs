using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Salidas
    {
        public int ID { get; set; }
        [Required]
        public int Empleado { get; set; }
        [Required]
        [Display(Name ="Tipo de Salida")]
        public opcion tipoSalida { get; set; }
        [Required]
        [StringLength(50)]
        public string Motivo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name ="Fecha de Salida")]
        public DateTime fechaSalida { get; set; }

        [ForeignKey("Empleado")]
        public Empleados Empleados { get; set; }
    }

    public enum opcion
    {
        Renuncia,
        Despido,
        Desahucio
    }
}
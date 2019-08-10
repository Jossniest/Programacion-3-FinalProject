using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Nominas
    {
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Año y Fecha")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM yyyy}")]
        public DateTime mesAno { get; set; }
        //[Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        //public DateTime Mes { get; set; }
        [Display(Name ="Monto Total")]
        public int montoTotal { get; set; }
    }
}
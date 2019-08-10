using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Departamentos
    {
        public int ID { get; set; }
        [Required]
        [Display(Name ="Codigo de Departamento")]
        [StringLength(20)]
        public string codigoDepartamento { get; set; }
        [Required]
        [StringLength(40)]
        [Display(Name ="Departamento")]
        public string Nombre { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Empleados
    {
        public int ID { get; set; }
        [Required]
        [Display(Name ="Codigo de Empleado")]
        [StringLength(20)]
        public string codigoEmpleado { get; set; }
        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(20)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(10)]
        public string Telefono { get; set; }
        [Required]
        public int Departamento { get; set; }
        
        [Required]
        public int Cargo { get; set; }

     

        [Required]
        public int Salario { get; set; }
        [Required]
        public bool Estatus { get; set; }

        [ForeignKey("Departamento")]
        public Departamentos Departamentos { get; set; }

        [ForeignKey("Cargo")]
        public Cargos Cargos { get; set; }

    }
}
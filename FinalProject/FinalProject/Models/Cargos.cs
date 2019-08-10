using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Cargos
    {
        public int ID { get; set; }
        [Required]
        [StringLength(25)]
        public string Cargo { get; set; }
    }
}
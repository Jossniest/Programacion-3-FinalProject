using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Empleados> empleado { get; set; }
        public DbSet<Departamentos> departamento { get; set; }
        public DbSet<Cargos> cargo { get; set; }
        public DbSet<Salidas> salida { get; set; }
        public DbSet<Licencias> licencia { get; set; }
        public DbSet<Permisos> permiso { get; set; }
        public DbSet<Nominas> nomina { get; set; }
        public DbSet<Observaciones> Observaciones { get; set; }

    }
}
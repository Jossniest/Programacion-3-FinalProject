using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;

namespace FinalProject.Controllers
{
    public class EmpleadosController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Empleados
        public ActionResult Index()
        {
            var empleado = db.empleado.Include(e => e.Cargos).Include(e => e.Departamentos); 
            return View(empleado.ToList());
        }

        public ViewResult Activos(string searchString)
        {  
            var empleado = db.empleado.Include(e => e.Cargos).Include(e => e.Departamentos);
            var employed = empleado.Where(s => s.Estatus == true );
            if (!String.IsNullOrEmpty(searchString)) {

                employed = empleado.Where(s => s.Estatus == true && 
                (s.Nombre.Contains(searchString) || s.Departamentos.Nombre.Contains(searchString)));

            }
            return View(employed.ToList());
        }

        public ActionResult Inactivos()
        {
            var empleado = db.empleado.Include(e => e.Cargos).Include(e => e.Departamentos);
            var employed = empleado.Where(s => s.Estatus == false);
            return View(employed.ToList());
        }

   

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.empleado.Find(id);
         
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo");
            ViewBag.Departamento = new SelectList(db.departamento, "ID", "Nombre");
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,codigoEmpleado,Nombre,Apellido,Telefono,Departamento,Cargo,fechaIngreso,Salario")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                empleados.Estatus = true;
                db.empleado.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.departamento, "ID", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.empleado.Find(id);
            
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.departamento, "ID", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,codigoEmpleado,Nombre,Apellido,Telefono,Departamento,Cargo,fechaIngreso,Salario,Estatus")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
   
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.departamento, "ID", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.empleado.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.empleado.Find(id);
            db.empleado.Remove(empleados);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

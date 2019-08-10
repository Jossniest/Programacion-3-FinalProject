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
    public class ObservacionesController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Observaciones
        public ActionResult Index()
        {
            var observaciones = db.Observaciones.Include(o => o.Empleados);
            return View(observaciones.ToList());
        }
        public ActionResult ObservacionesEmpleado(String searchString)
        {
            var lista = from a in db.Observaciones
                        select a;

            lista = lista.Where(s => s.Empleados.codigoEmpleado.Contains(searchString));

            return View(lista.ToList());
        }

        // GET: Observaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Observaciones observaciones = db.Observaciones.Find(id);
            if (observaciones == null)
            {
                return HttpNotFound();
            }
            return View(observaciones);
        }

        // GET: Observaciones/Create
        public ActionResult Create()
        {
            ViewBag.Empleado = new SelectList(db.empleado, "ID", "codigoEmpleado");
            return View();
        }

        // POST: Observaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Empleado,Observacion,Fecha,Comentarios")] Observaciones observaciones)
        {
            if (ModelState.IsValid)
            {
                db.Observaciones.Add(observaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empleado = new SelectList(db.empleado, "ID", "codigoEmpleado", observaciones.Empleado);
            return View(observaciones);
        }

        // GET: Observaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Observaciones observaciones = db.Observaciones.Find(id);
            if (observaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empleado = new SelectList(db.empleado, "ID", "codigoEmpleado", observaciones.Empleado);
            return View(observaciones);
        }

        // POST: Observaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Empleado,Observacion,Fecha,Comentarios")] Observaciones observaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(observaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empleado = new SelectList(db.empleado, "ID", "codigoEmpleado", observaciones.Empleado);
            return View(observaciones);
        }

        // GET: Observaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Observaciones observaciones = db.Observaciones.Find(id);
            if (observaciones == null)
            {
                return HttpNotFound();
            }
            return View(observaciones);
        }

        // POST: Observaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Observaciones observaciones = db.Observaciones.Find(id);
            db.Observaciones.Remove(observaciones);
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

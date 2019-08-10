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
    public class SalidasController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Salidas
        public ActionResult Index()
        {

            var salida = db.salida.Include(s => s.Empleados);
            return View(salida.ToList());
        }

        public ActionResult SalidaMes(string searchString)
        {
            var lista = from a in db.salida
                        select a;

            lista = lista.Where(s => s.fechaSalida.Month.ToString().Contains(searchString));

            return View(lista.Include(s => s.Empleados).ToList());
        }

        // GET: Salidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salidas salidas = db.salida.Find(id);
            if (salidas == null)
            {
                return HttpNotFound();
            }
            return View(salidas);
        }

        // GET: Salidas/Create
        public ActionResult Create()
        {
            ViewBag.Empleado = new SelectList(db.empleado.Where(s=>s.Estatus==true), "ID", "codigoEmpleado");
            return View();
        }

        // POST: Salidas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Empleado,tipoSalida,Motivo,fechaSalida")] Salidas salidas)
        {
            if (ModelState.IsValid)
            {
                var query = (from a in db.empleado
                             where a.ID == salidas.Empleado
                             select a).First();

                query.Estatus = false;

                db.salida.Add(salidas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empleado = new SelectList(db.empleado, "ID", "codigoEmpleado", salidas.Empleado);
            return View(salidas);
        }

        // GET: Salidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salidas salidas = db.salida.Find(id);
            if (salidas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empleado = new SelectList(db.empleado, "ID", "codigoEmpleado", salidas.Empleado);
            return View(salidas);
        }

        // POST: Salidas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Empleado,tipoSalida,Motivo,fechaSalida")] Salidas salidas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salidas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empleado = new SelectList(db.empleado, "ID", "codigoEmpleado", salidas.Empleado);
            return View(salidas);
        }

        // GET: Salidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salidas salidas = db.salida.Find(id);
            if (salidas == null)
            {
                return HttpNotFound();
            }
            return View(salidas);
        }

        // POST: Salidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            Salidas salidas = db.salida.Find(id);

            Empleados empleado = (from a in db.empleado where a.ID == salidas.Empleado select a).First();

            empleado.Estatus = true;
            db.salida.Remove(salidas);
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

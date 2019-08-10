using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
using System.Data.SqlClient;
using OfficeOpenXml;
using System.Drawing;

namespace FinalProject.Controllers
{
    public class NominasController : Controller
    {
        private ProjectContext db = new ProjectContext();
      
        // GET: Nominas
        public ActionResult Index()
        {
            return View(db.nomina.ToList());
        }
        public void ExportExcel(DateTime fecha)
        {
            List<Empleados> empleadoslist = db.empleado.Where(s => s.Estatus == true).Include(s => s.Cargos).ToList();
            /*List<Empleados> empleadoslist = db.empleado.Select(x => new Empleados
            {
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Salario = x.Salario,
                Cargo = x.Cargo
            }).ToList();*/
            int total = empleadoslist.Sum(x => x.Salario);

            ExcelPackage package = new ExcelPackage();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report");

            
            worksheet.Cells["D1"].Value = "Nomina";
            worksheet.Cells["C2"].Value = "Lista de nomina hasta la fecha";
            worksheet.Cells["C3"].Value = "Fecha de Nomina Generada";
            worksheet.Cells["D3"].Value = fecha.ToString();

            worksheet.Cells["C6"].Value = "Empleado Nombre";
            worksheet.Cells["D6"].Value = "Empleado Apellido";
            worksheet.Cells["E6"].Value = "Salario";
            worksheet.Cells["F6"].Value = "Cargo";

            worksheet.Cells["E4"].Value = "Total =";
            worksheet.Cells["F4"].Value = total;
            int rowStart = 7; 
            foreach (var item in empleadoslist)
            {
                worksheet.Cells[string.Format("C{0}", rowStart)].Value = item.Nombre;
                worksheet.Cells[string.Format("D{0}", rowStart)].Value = item.Apellido;
                worksheet.Cells[string.Format("E{0}", rowStart)].Value = item.Salario;
                worksheet.Cells[string.Format("F{0}", rowStart)].Value = item.Cargos.Cargo;
                rowStart++;
            }
            worksheet.Cells["A:AZ"].AutoFitColumns();
            worksheet.Cells.Style.Font.Name = "Arial";
            worksheet.Cells.Style.Font.Size = 12;
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(package.GetAsByteArray());
            Response.End();



              

        }

        public ActionResult Busqueda(string fecha)
        {
            var lista = from a in db.nomina
                         select a;
            lista = lista.Where(s => s.mesAno.ToString().Contains(fecha) );
            
            
            return View(lista.ToList());
        }



        // GET: Nominas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nominas nominas = db.nomina.Find(id);
            if (nominas == null)
            {
                return HttpNotFound();
            }
            return View(nominas);
        }

        // GET: Nominas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nominas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,mesAno,montoTotal")] Nominas nominas)
        {
            if (ModelState.IsValid)
            {
                nominas.montoTotal= db.empleado.Where(s=>s.Estatus==true).Sum(a => a.Salario);
                db.nomina.Add(nominas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nominas);
        }

        // GET: Nominas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nominas nominas = db.nomina.Find(id);
            if (nominas == null)
            {
                return HttpNotFound();
            }
            return View(nominas);
        }

        // POST: Nominas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,mesAno,montoTotal")] Nominas nominas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nominas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nominas);
        }

        // GET: Nominas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nominas nominas = db.nomina.Find(id);
            if (nominas == null)
            {
                return HttpNotFound();
            }
            return View(nominas);
        }

        // POST: Nominas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nominas nominas = db.nomina.Find(id);
            db.nomina.Remove(nominas);
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

using LinqTransportes.DAO;
using LinqTransportes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinqTransportes.Controllers
{
    public class TrabajadorController : Controller
    {
        private void GetData()
        {
            TrabajadorDAO.GetData();
        }

        // GET: Trabajador
        public ActionResult Index()
        {
            GetData();
            List<Trabajador> lista = Statics.Datos.Trabajadores.Select(x => x.Value).ToList();
            return View(lista);
        }

        // GET: Trabajador/Create
        public ActionResult Create()
        {
            GetData();
            List<Puesto> lista = Statics.Datos.Puestos.Select(x => x.Value).ToList();
            ViewBag.Puestos = lista;
            return View();
        }

        // POST: Trabajador/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            GetData();
            List<Puesto> lista = Statics.Datos.Puestos.Select(x => x.Value).ToList();
            ViewBag.Puestos = lista;
            try
            {
                int nId = TrabajadorDAO.GetNextIdentity();
                Trabajador tr = new Trabajador
                {
                    ID = nId,
                    Nombres = collection["Nombres"],
                    APaterno = collection["APaterno"],
                    AMaterno = collection["AMaterno"],
                    Rut = collection["Rut"],
                    Telefono = collection["Telefono"],
                    Direccion = collection["Direccion"],
                    Email = collection["Email"],
                    Fecha = DateTime.Now,
                    Puesto = Statics.Datos.Puestos[int.Parse(collection["Puesto"])]
                };
                Statics.Datos.Trabajadores.Add(nId, tr);

                TrabajadorDAO.Insert(tr);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Trabajador/Edit/5
        public ActionResult Edit(int id)
        {
            GetData();
            List<Puesto> lista = Statics.Datos.Puestos.Select(x => x.Value).ToList();
            ViewBag.Puestos = lista;
            ViewBag.Puesto = Statics.Datos.Trabajadores[id].Puesto.Id;
            Trabajador tr = Statics.Datos.Trabajadores[id];
            return View(tr);
        }

        // POST: Trabajador/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            GetData();
            try
            {
                Trabajador tr = new Trabajador
                {
                    ID = id,
                    Nombres = collection["Nombres"],
                    APaterno = collection["APaterno"],
                    AMaterno = collection["AMaterno"],
                    Rut = collection["Rut"],
                    Telefono = collection["Telefono"],
                    Direccion = collection["Direccion"],
                    Email = collection["Email"],
                    Fecha = DateTime.Now,
                    Puesto = Statics.Datos.Puestos[int.Parse(collection["Puesto"])]
                };
                Statics.Datos.Trabajadores[id] = tr;

                TrabajadorDAO.Update(tr);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Trabajador/Delete/5
        public ActionResult Delete(int id)
        {
            GetData();
            Trabajador tr = Statics.Datos.Trabajadores[id];
            return View(tr);
        }

        // POST: Trabajador/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            GetData();
            try
            {
                TrabajadorDAO.Delete(id);
                Statics.Datos.Trabajadores.Remove(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

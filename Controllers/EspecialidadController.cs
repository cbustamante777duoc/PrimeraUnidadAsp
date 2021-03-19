using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppNetCore.Clases;
using MiPrimeraAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Controllers
{
    public class EspecialidadController : Controller
    {
        public IActionResult Index(EspecialidadCLS oEspecialidadCLS)
        {
            List<EspecialidadCLS> ListaEspecialidad = new List<EspecialidadCLS>();
            //ViewBag.mensaje = "mensaje de prueba";
            using (BDHospitalContext db = new BDHospitalContext()) 
            {
                if (oEspecialidadCLS.nombre == null || oEspecialidadCLS.nombre == "")
                {
                    ListaEspecialidad = (from especialidad in db.Especialidad
                                         where especialidad.Bhabilitado == 1
                                         select new EspecialidadCLS
                                         {
                                             iidespecilidad = especialidad.Iidespecialidad,
                                             nombre = especialidad.Nombre,
                                             descripcion = especialidad.Descripcion
                                         }).ToList();
                    ViewBag.nombreEspecialidad = "";
                }
                else
                {
                    ListaEspecialidad = (from especialidad in db.Especialidad
                                         where especialidad.Bhabilitado == 1
                                         && especialidad.Nombre.Contains(oEspecialidadCLS.nombre)
                                         select new EspecialidadCLS
                                         {
                                             iidespecilidad = especialidad.Iidespecialidad,
                                             nombre = especialidad.Nombre,
                                             descripcion = especialidad.Descripcion
                                         }).ToList();
                    //para guardar lo que el usuario registro en la busqueda
                    ViewBag.nombreEspecialidad = oEspecialidadCLS.nombre;
                }
                
            }
            return View(ListaEspecialidad);
        }

        public IActionResult Agregar() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(EspecialidadCLS oEspecialidadCLS)
        {
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    //si no es valido
                    if (!ModelState.IsValid)
                    {
                        //para conservar los datos que el usuario escribio
                        return View(oEspecialidadCLS);
                    }
                    else
                    {
                        //instacia del modelo y guarda los datos
                        Especialidad objeto = new Especialidad();
                        objeto.Nombre = oEspecialidadCLS.nombre;
                        objeto.Descripcion = oEspecialidadCLS.descripcion;
                        objeto.Bhabilitado = 1;
                        db.Especialidad.Add(objeto);
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {

                //para conservar los datos que el usuario escribio
                return View(oEspecialidadCLS);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        //el iidespecilidad lo recibe de la vista 
        public IActionResult Eliminar(int iidespecilidad) 
        {
            string error;

            try
            {
                using (BDHospitalContext bd = new BDHospitalContext())
                {
                    Especialidad especialidad = bd.Especialidad
                        .Where(p => p.Iidespecialidad == iidespecilidad).First();

                    especialidad.Bhabilitado = 0;
                    bd.SaveChanges();

                }

            }
            catch (Exception ex)
            {

                error = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id) 
        {
            EspecialidadCLS oEspecialidadCLS = new EspecialidadCLS();
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                oEspecialidadCLS = (from especialidad in bd.Especialidad
                                    where especialidad.Iidespecialidad == id
                                    select new EspecialidadCLS
                                    {
                                        iidespecilidad = especialidad.Iidespecialidad,
                                        nombre = especialidad.Nombre,
                                        descripcion = especialidad.Descripcion
                                    }).First();
            }

            return View(oEspecialidadCLS);
        
        }
    }
}

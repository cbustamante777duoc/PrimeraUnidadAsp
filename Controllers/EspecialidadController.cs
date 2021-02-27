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
    }
}

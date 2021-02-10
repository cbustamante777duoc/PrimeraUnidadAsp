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
        public IActionResult Index()
        {
            List<EspecialidadCLS> ListaEspecialidad = new List<EspecialidadCLS>();
            using (BDHospitalContext db = new BDHospitalContext()) 
            {
                ListaEspecialidad = (from especialidad in db.Especialidad
                                     where especialidad.Bhabilitado == 1
                                     select new EspecialidadCLS
                                     {
                                         iidespecilidad = especialidad.Iidespecialidad,
                                         nombre = especialidad.Nombre,
                                         descripcion = especialidad.Descripcion
                                     }).ToList();
            }
            return View(ListaEspecialidad);
        }
    }
}

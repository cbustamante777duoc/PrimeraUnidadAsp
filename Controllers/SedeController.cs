using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppNetCore.Clases;
using MiPrimeraAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Controllers
{
    public class SedeController : Controller
    {
        public IActionResult Index()
        {
            List<SedeCLS> listaSede = new List<SedeCLS>();
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                listaSede = (from sede in bd.Sede
                             where sede.Bhabilitado == 1
                             select new SedeCLS
                             {
                                 iidSede = sede.Iidsede,
                                 nombreSede = sede.Nombre,
                                 direcion = sede.Direccion
                             }).ToList();
                
            }
            return View(listaSede);
        }
    }
}

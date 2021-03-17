﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(SedeCLS oSedeCLS)
        {
            List<SedeCLS> listaSede = new List<SedeCLS>();
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                if (oSedeCLS.nombreSede == "" || oSedeCLS.nombreSede == null)
                {
                    listaSede = (from sede in bd.Sede
                                 where sede.Bhabilitado == 1
                                 select new SedeCLS
                                 {
                                     iidSede = sede.Iidsede,
                                     nombreSede = sede.Nombre,
                                     direcion = sede.Direccion
                                 }).ToList();
                    ViewBag.nombreSede = "";
                }
                else
                {
                    listaSede = (from sede in bd.Sede
                                 where sede.Bhabilitado == 1
                                 && sede.Nombre.Contains(oSedeCLS.nombreSede)
                                 select new SedeCLS
                                 {
                                     iidSede = sede.Iidsede,
                                     nombreSede = sede.Nombre,
                                     direcion = sede.Direccion
                                 }).ToList();
                    ViewBag.nombreSede = oSedeCLS.nombreSede;
                }
                
                
            }
            return View(listaSede);
        }

        [HttpPost]
        public IActionResult Eliminar(int iidSede) 
        {
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                Sede sede = bd.Sede.Where(p => p.Iidsede == iidSede).First();
                sede.Bhabilitado = 0;
                bd.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}

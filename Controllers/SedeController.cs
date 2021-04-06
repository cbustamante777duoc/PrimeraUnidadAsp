using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppNetCore.Clases;
using MiPrimeraAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Controllers
{
    public class SedeController : BaseController
    {
        public static List<SedeCLS> lista;
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
            lista = listaSede;
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
        
        //recupera la vista con los datos
        public IActionResult Editar(int id) 
        {
            SedeCLS sedeCLS = new SedeCLS();
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                sedeCLS = (from sede in bd.Sede
                           where sede.Iidsede == id
                           select new SedeCLS
                           {
                               iidSede = sede.Iidsede,
                               nombreSede = sede.Nombre,
                               direcion = sede.Direccion
                           }).First();

            }

            return View(sedeCLS);
        }

        [HttpPost]
        public IActionResult Guardar(SedeCLS oSedeCLS) 
        {
            string nombreVista = "";
            if (oSedeCLS.iidSede == 0) nombreVista = "Agregar";
            else nombreVista = "Editar";

            if (!ModelState.IsValid)
            {
                return View(nombreVista, oSedeCLS);
            }
            else
            {
                using (BDHospitalContext bd = new BDHospitalContext())
                {
                    if (oSedeCLS.iidSede !=0)
                    {
                        Sede sede = bd.Sede.Where(p => p.Iidsede == oSedeCLS.iidSede)
                                            .First();
                        sede.Nombre = oSedeCLS.nombreSede;
                        sede.Direccion = oSedeCLS.direcion;
                        bd.SaveChanges();

                    }

                }
                //editar

            }
            return RedirectToAction("Index");
        
        }

        //metodo para descargar
        public FileResult Exportar(string[] nombrePropiedades, string tipoReporte)
        {
            //cabeceras para el excel(parte de arriba)
            //string[] cabeceras = { "Id Especialidad", "Nombre", "Descripcion" };
            //string[] nombrePropiedades = { "iidespecilidad", "nombre", "descripcion" };

            if (tipoReporte == "Excel")
            {

                byte[] buffer = exportarExcelDatos(nombrePropiedades, lista);
                return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            }
            else if (tipoReporte == "PDF")
            {
                byte[] buffer = exportarPDFDatos(nombrePropiedades, lista);
                return File(buffer, "application/pdf");
            }
            else if (tipoReporte == "Word")
            {
                byte[] buffer = exportarDatosWord(nombrePropiedades, lista);
                return File(buffer, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            }

            return null;

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppNetCore.Clases;
using MiPrimeraAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using cm = System.ComponentModel;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;

namespace MiPrimeraAppNetCore.Controllers
{
    public class EspecialidadController : BaseController
    {
        public static List<EspecialidadCLS> lista;
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
            //guardo todo en la lista estatica
            lista = ListaEspecialidad;
            return View(ListaEspecialidad);
        }

        public IActionResult Agregar() 
        {
            return View();
        }

        [HttpPost]
        /*
         * en le guardar el iidespecilidad es 0
         * en le editar el id tiene un valor porque lo trae de la vista(index)
         */
        public IActionResult Guardar(EspecialidadCLS oEspecialidadCLS)
        {
            string nombreVista = "";
            int numeroVeces = 0;

            try
            {
                //corregiendo el error de la vista guardar
                if (oEspecialidadCLS.iidespecilidad == 0) nombreVista = "Agregar";
                else nombreVista = "Editar";

                using (BDHospitalContext db = new BDHospitalContext())
                {
                    //validacion para ir a al GUARDAR
                    if (oEspecialidadCLS.iidespecilidad == 0)
                        //analizo cuantas veces se repite el nombre de especialidad en la bd
                        //se se pasan ambas a mayuscula
                        numeroVeces = db.Especialidad
                            .Where(p => p.Nombre.ToUpper().Trim() == oEspecialidadCLS.nombre.ToUpper().Trim())
                            .Count();
                    else
                        //validacion para el EDITAR
                        numeroVeces = db.Especialidad
                            .Where(p => p.Nombre.ToUpper().Trim() ==
                            oEspecialidadCLS.nombre.ToUpper().Trim() && 
                            p.Iidespecialidad != oEspecialidadCLS.iidespecilidad )
                            .Count(); 
                            

                    //si no es valido el modelo o se repite los valores en la bd
                    if (!ModelState.IsValid || numeroVeces >=1)
                    {
                        if (numeroVeces >= 1) 
                            oEspecialidadCLS.mensajeError = "el nombre de la especialida ya existe";
                        //para conservar los datos que el usuario escribio y la vista
                        return View(nombreVista,oEspecialidadCLS);
                    }
                    else
                    {
                        //GUARDAR EN BD
                        if (oEspecialidadCLS.iidespecilidad==0)
                        {

                            //instacia del modelo y guarda los datos
                            Especialidad objeto = new Especialidad();
                            objeto.Nombre = oEspecialidadCLS.nombre;
                            objeto.Descripcion = oEspecialidadCLS.descripcion;
                            objeto.Bhabilitado = 1;
                            db.Especialidad.Add(objeto);
                            db.SaveChanges();
                        }
                        //EDITAR EN BD
                        else
                        {
                            Especialidad objeto = db.Especialidad
                                                  .Where(p => p.Iidespecialidad == oEspecialidadCLS.iidespecilidad)
                                                  .First();
                            objeto.Nombre = oEspecialidadCLS.nombre;
                            objeto.Descripcion = oEspecialidadCLS.descripcion;
                            db.SaveChanges();

                        }
                    }

                }
            }
            catch (Exception)
            {

                //para conservar los datos que el usuario escribio y consevar la vista
                return View(nombreVista,oEspecialidadCLS);
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


        //recupera la informacion
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

        //public string exportarPDFDatos(string[] nombrePropiedades)
        //{
        //    byte[] buffer = exportarPDFDatos(nombrePropiedades, lista);

        //    string cadena = Convert.ToBase64String(buffer);
        //    //para que descarge
        //    cadena = "data:application/pdf;base64," + cadena;

        //    return cadena;
        //}






    }
}

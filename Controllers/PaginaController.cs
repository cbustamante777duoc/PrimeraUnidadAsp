using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppNetCore.Clases;
using MiPrimeraAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Controllers
{
    public class PaginaController : Controller
    {
        public IActionResult Index(PaginaCLS oPaginaCLS)
        {
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                //se valida 
                if (oPaginaCLS.mensaje==null || oPaginaCLS.mensaje=="")
                {
                    listaPagina = (from pagina in bd.Pagina
                                   where pagina.Bhabilitado == 1
                                   select new PaginaCLS
                                   {
                                       iidPagina = pagina.Iidpagina,
                                       mensaje = pagina.Mensaje,
                                       accion = pagina.Accion,
                                       controlador = pagina.Controlador
                                   }).ToList();
                    ViewBag.mensaje = "";

                }
                //se filtra
                else
                {
                    listaPagina = (from pagina in bd.Pagina
                                   where pagina.Bhabilitado == 1
                                   && pagina.Mensaje.Contains(oPaginaCLS.mensaje)
                                   select new PaginaCLS
                                   {
                                       iidPagina = pagina.Iidpagina,
                                       mensaje = pagina.Mensaje,
                                       accion = pagina.Accion,
                                       controlador = pagina.Controlador
                                   }).ToList();

                    ViewBag.mensaje = oPaginaCLS.mensaje;

                }
               

              


            }
                return View(listaPagina);
        }

        //mostrar el formulario
        public IActionResult Agregar() 
        {

            return View();
        }

        //guardar los datos
        [HttpPost]
        public IActionResult Agregar(PaginaCLS oPaginaCLS) 
        {
            try
            {
                using (BDHospitalContext bd = new BDHospitalContext())
                {
                    //si no ingresa datos el usuario
                    if (!ModelState.IsValid)
                    {
                        //se queda en la misma pagina
                        return View(oPaginaCLS);
                    }
                    else
                    {
                        //instancia del modelo
                        Pagina pagina = new Pagina();
                        //se igualan los atributos del modelo con la clase
                        pagina.Mensaje = oPaginaCLS.mensaje;
                        pagina.Controlador = oPaginaCLS.controlador;
                        pagina.Accion = oPaginaCLS.accion;
                        //solo los habilitados
                        pagina.Bhabilitado = 1;
                        //se guarda en base de datos el modelo
                        bd.Pagina.Add(pagina);
                        //se confirma y se guardan los cambios
                        bd.SaveChanges();

                    }


                    

                }
            }
            catch (Exception)
            {
                //si hay un error se queda en la misma pagina
                return View(oPaginaCLS);
            }
            return RedirectToAction("Index");
        }
    }
}

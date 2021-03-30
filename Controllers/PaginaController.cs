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
        public IActionResult Guardar(PaginaCLS oPaginaCLS) 
        {
            string nombreVista = "";
            int nveces = 0;

            try
            {
                if (oPaginaCLS.iidPagina == 0) nombreVista = "Agregar";
                else nombreVista = "Editar";

                    using (BDHospitalContext bd = new BDHospitalContext())
                    {

                        //validacion para el agregar
                        if (oPaginaCLS.iidPagina==0)
                        {
                            nveces = bd.Pagina
                            .Where(p => p.Mensaje.ToUpper().Trim() ==
                            oPaginaCLS.mensaje.ToUpper().Trim())
                            .Count();
                            
                        }
                        else
                        {
                            //validacion para el modificar
                            nveces = bd.Pagina
                            .Where(p => p.Mensaje.ToUpper().Trim() ==
                            oPaginaCLS.mensaje.ToUpper().Trim() &&
                            p.Iidpagina != oPaginaCLS.iidPagina ) 
                            .Count();
                        }
                        
                        //si no ingresa datos el usuario
                        if (!ModelState.IsValid || nveces>=1)
                        {

                        if (nveces >= 1) oPaginaCLS.mensajeError = 
                                "ya existe el mensaje de la pagina ingresada";
                            return View(nombreVista,oPaginaCLS);
                        }
                        else
                        {
                            //GUARDA LOS DATOS EN BD
                            if (oPaginaCLS.iidPagina==0)
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
                            else
                            {
                                Pagina pagina = bd.Pagina
                                  .Where(p => p.Iidpagina == oPaginaCLS.iidPagina)
                                  .First();

                                pagina.Mensaje = oPaginaCLS.mensaje;
                                pagina.Controlador = oPaginaCLS.controlador;
                                pagina.Accion = oPaginaCLS.controlador;
                                bd.SaveChanges();
                            }

                        }




                    }
            }
            catch (Exception)
            {
                //si hay un error se queda en la misma pagina
                return View(nombreVista, oPaginaCLS);
            }
            return RedirectToAction("Index");
        }

            /**
             * metodo que que rescapa todos los valores del modelo
             */
        public IActionResult Editar(int id) 
        {
            PaginaCLS oPaginaCLS = new PaginaCLS();
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                oPaginaCLS = (from pagina in bd.Pagina
                              where pagina.Iidpagina == id
                              select new PaginaCLS
                              {
                                  iidPagina = pagina.Iidpagina,
                                  mensaje = pagina.Mensaje,
                                  accion = pagina.Accion,
                                  controlador = pagina.Controlador
                              }).First();

            }
            return View(oPaginaCLS);
        }


        [HttpPost]
        //eliminacion fisica
        public IActionResult Eliminar(int iidPagina)
        {
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                Pagina pagina = bd.Pagina
                               .Where(p => p.Iidpagina == iidPagina).First();
                bd.Pagina.Remove(pagina);
                bd.SaveChanges();


            }

            return RedirectToAction("Index");
               
        }

    }
}

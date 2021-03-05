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
    }
}

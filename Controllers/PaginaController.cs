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
        public IActionResult Index()
        {
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();
            using (BDHospitalContext bd = new BDHospitalContext())
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
            
            }
                return View(listaPagina);
        }
    }
}

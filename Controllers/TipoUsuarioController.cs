using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiPrimeraAppNetCore.Models;
using MiPrimeraAppNetCore.Clases;

namespace MiPrimeraAppNetCore.Controllers
{
    public class TipoUsuarioController : Controller
    {
        public IActionResult Index(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            List<TipoUsuarioCLS> listaTipoUsuario = new List<TipoUsuarioCLS>();
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                listaTipoUsuario = (from tipousu in bd.TipoUsuario
                                    where tipousu.Bhabilitado == 1
                                    select new TipoUsuarioCLS
                                    {
                                        iidTipoUsuario = tipousu.Iidtipousuario,
                                        nombre = tipousu.Nombre,
                                        descripcion = tipousu.Descripcion
                                    }).ToList();
                //pregunta si no tiene nada los valores de los inputs
                if (oTipoUsuarioCLS.nombre == null && oTipoUsuarioCLS.descripcion == null
                    && oTipoUsuarioCLS.iidTipoUsuario == 0)
                {
                    ViewBag.nombre = "";
                    ViewBag.descripcion = "";
                    ViewBag.IidTipoUsuario = 0;
                }
                //ahora se hace el filtrado
                else 
                {
                    //filtra por cada condicion
                    if (oTipoUsuarioCLS.nombre != null)
                    {
                        listaTipoUsuario = listaTipoUsuario
                            .Where(p => p.nombre.Contains(oTipoUsuarioCLS.nombre))
                            .ToList();
                    }

                    if (oTipoUsuarioCLS.iidTipoUsuario != 0)
                    {
                        listaTipoUsuario = listaTipoUsuario
                            .Where(p => p.iidTipoUsuario == oTipoUsuarioCLS.iidTipoUsuario)
                            .ToList();
                    }

                    if (oTipoUsuarioCLS.descripcion != null)
                    {
                        listaTipoUsuario = listaTipoUsuario
                            .Where(p => p.descripcion.Contains(oTipoUsuarioCLS.descripcion))
                            .ToList();
                    }

                    //se guardan los valores de las busquedas ingresadas por los usuarios de la aplicacion
                    ViewBag.nombre = oTipoUsuarioCLS.nombre;
                    ViewBag.descripcion = oTipoUsuarioCLS.descripcion;
                    ViewBag.IidTipoUsuario = oTipoUsuarioCLS.iidTipoUsuario;

                }
            }
            return View(listaTipoUsuario);
        }
    }
}

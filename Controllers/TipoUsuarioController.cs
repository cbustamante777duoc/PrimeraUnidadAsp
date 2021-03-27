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

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            //se pone a fuera del try para que el nombrevista llege hasta el catch
                string nombreVista = "";
                int nvecesNombre = 0;
                int nvecesDescripcion = 0;
            try
            {
                //validaciones al tipo vista
                if (oTipoUsuarioCLS.iidTipoUsuario == 0) nombreVista = "Agregar";
                else nombreVista = "Editar";

                using (BDHospitalContext bd = new BDHospitalContext())
                {
                    //en el caso de agregar
                    if (oTipoUsuarioCLS.iidTipoUsuario == 0)
                    {
                        //validacion de repeticiones en la bd nombre
                        nvecesNombre = bd.TipoUsuario
                            .Where(p => p.Nombre.ToUpper().Trim() ==
                            oTipoUsuarioCLS.nombre.ToUpper().Trim())
                            .Count();

                        //validacion de repeticiones en la bd descripcion
                        nvecesDescripcion = bd.TipoUsuario
                            .Where(p => p.Descripcion.ToUpper().Trim() ==
                            oTipoUsuarioCLS.descripcion.ToUpper().Trim())
                            .Count();
                    }
                    else 
                    {
                        //validacion de repeticiones en la bd nombre
                        nvecesNombre = bd.TipoUsuario
                            .Where(p => p.Nombre.ToUpper().Trim() ==
                            oTipoUsuarioCLS.nombre.ToUpper().Trim())
                            .Count();

                        //validacion de repeticiones en la bd descripcion
                        nvecesDescripcion = bd.TipoUsuario
                            .Where(p => p.Descripcion.ToUpper().Trim() ==
                            oTipoUsuarioCLS.descripcion.ToUpper().Trim() &&
                            p.Iidtipousuario != oTipoUsuarioCLS.iidTipoUsuario)
                            .Count();

                    }

                    if (!ModelState.IsValid || nvecesNombre>=1 || nvecesDescripcion>=1)
                    {
                        //mensaje de error
                        if (nvecesNombre >= 1)
                            oTipoUsuarioCLS.mensajeErrorNombre = "el nombre ya existe en la bd";

                        if (nvecesDescripcion >= 1)
                            oTipoUsuarioCLS.mensajeErrorDescripcion = "la descripcion ya existe en la bd";

                        return View(nombreVista, oTipoUsuarioCLS);
                    }
                    else
                    {


                        TipoUsuario tipoUsuario = new TipoUsuario();
                        tipoUsuario.Nombre = oTipoUsuarioCLS.nombre;
                        tipoUsuario.Descripcion = oTipoUsuarioCLS.descripcion;
                        tipoUsuario.Bhabilitado = 1;
                        bd.TipoUsuario.Add(tipoUsuario);
                        bd.SaveChanges();

                    }

                }

            }
            catch (Exception)
            {

                return View(nombreVista, oTipoUsuarioCLS);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        //eliminacion fisica 
        public IActionResult Eliminar(int iidTipoUsuario)
        {
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                TipoUsuario tipoUsuario = bd.TipoUsuario
                                  .Where(p => p.Iidtipousuario == iidTipoUsuario)
                                  .First();
                bd.TipoUsuario.Remove(tipoUsuario);
                bd.SaveChanges();

            }

            return RedirectToAction("Index");
        }
    }
}

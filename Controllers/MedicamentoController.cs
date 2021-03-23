using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiPrimeraAppNetCore.Clases;
using MiPrimeraAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Controllers
{
    public class MedicamentoController : Controller
    {

        public List<SelectListItem> listarFormaFarmaceutica()
        {
            List<SelectListItem> listaFormaFarmaceutica = new List<SelectListItem>();
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                listaFormaFarmaceutica = (from formaFarmaceutica in bd.FormaFarmaceutica
                                          where formaFarmaceutica.Bhabilitado == 1
                                          select new SelectListItem
                                          {
                                              Text = formaFarmaceutica.Nombre,
                                              Value = formaFarmaceutica.Iidformafarmaceutica.ToString()
                                          }).ToList();
                listaFormaFarmaceutica.Insert(0, new SelectListItem { 
                    Text = "--Seleccione--",
                    Value = "" 
                });
                return listaFormaFarmaceutica;
            }

        }
        public IActionResult Index(MedicamentoCLS oMedicamentoCLS)
        {
            //llamada del metodo para que aparesca desde el inicio
            ViewBag.listaForma = listarFormaFarmaceutica();

            List<MedicamentoCLS> listaMedicamento = new List<MedicamentoCLS>();
            using (BDHospitalContext bd = new BDHospitalContext()) 
            {
                if ( oMedicamentoCLS.iidFormaFarmaceutica==null|| oMedicamentoCLS.iidFormaFarmaceutica == 0)
                {
                    listaMedicamento = (from medicamento in bd.Medicamento
                                        join formaFarmaceutica in bd.FormaFarmaceutica
                                        on medicamento.Iidformafarmaceutica equals
                                        formaFarmaceutica.Iidformafarmaceutica
                                        where medicamento.Bhabilitado ==1
                                        select new MedicamentoCLS
                                        {
                                            iidMedicamento = medicamento.Iidmedicamento,
                                            nombre = medicamento.Nombre,
                                            precio = (decimal)medicamento.Precio,
                                            stock = (int)medicamento.Stock,
                                            nombreFormaFarmaceutica = formaFarmaceutica.Nombre
                                        }).ToList();
                }
                else
                {
                    listaMedicamento = (from medicamento in bd.Medicamento
                                        join formaFarmaceutica in bd.FormaFarmaceutica
                                        on medicamento.Iidformafarmaceutica equals
                                        formaFarmaceutica.Iidformafarmaceutica
                                        where medicamento.Bhabilitado == 1 
                                        && 
                                        medicamento.Iidformafarmaceutica == oMedicamentoCLS.iidFormaFarmaceutica
                                        select new MedicamentoCLS
                                        {
                                            iidMedicamento = medicamento.Iidmedicamento,
                                            nombre = medicamento.Nombre,
                                            precio = (decimal)medicamento.Precio,
                                            stock = (int)medicamento.Stock,
                                            nombreFormaFarmaceutica = formaFarmaceutica.Nombre
                                        }).ToList();
                }
            }
                return View(listaMedicamento);
        }

        public IActionResult Agregar() 
        {
            ViewBag.listaFormaFarmaceutica = listarFormaFarmaceutica();
            return View();
        }

        //recuperar la informacion 
        public IActionResult Editar(int id)
        {
            MedicamentoCLS oMedicamentoCLS = new MedicamentoCLS();

            using (BDHospitalContext bd = new BDHospitalContext())
            {
                //regresa el objeto que tenga todos los datos que da el id
                oMedicamentoCLS = (from medicamento in bd.Medicamento
                                   where medicamento.Iidmedicamento == id
                                   select new MedicamentoCLS
                                   {
                                       iidMedicamento = medicamento.Iidmedicamento,
                                       nombre = medicamento.Nombre,
                                       concentracion = medicamento.Concentracion,
                                       iidFormaFarmaceutica = medicamento.Iidformafarmaceutica,
                                       precio = medicamento.Precio,
                                       stock = medicamento.Stock,
                                       presentacion = medicamento.Presentacion
                                   }).First();
            }
            ViewBag.listaFormaFarmaceutica = listarFormaFarmaceutica();
            return View(oMedicamentoCLS);
        }


        [HttpPost]
        public IActionResult Guardar (MedicamentoCLS oMedicamentoCLS) 
        {
            string nombreVista = "";

            try
            {
                using (BDHospitalContext bd = new BDHospitalContext())
                {
                    if (oMedicamentoCLS.iidMedicamento == 0) nombreVista = "Agregar";
                    else nombreVista = "Editar";

                    //si no son validos los datos se queda en la vista
                    if (!ModelState.IsValid)
                    {
                        //variable que iguala al metodo de la forma farmaceutica
                        ViewBag.listaFormaFarmaceutica = listarFormaFarmaceutica();
                        return View(nombreVista, oMedicamentoCLS);
                    }
                    else
                    {
                        //verfica si la id es 0 es guardar
                        if (oMedicamentoCLS.iidMedicamento == 0)
                        {
                            Medicamento medicamento = new Medicamento();
                            medicamento.Nombre = oMedicamentoCLS.nombre;
                            medicamento.Concentracion = oMedicamentoCLS.concentracion;
                            medicamento.Iidformafarmaceutica = oMedicamentoCLS.iidFormaFarmaceutica;
                            medicamento.Precio = oMedicamentoCLS.precio;
                            medicamento.Stock = oMedicamentoCLS.stock;
                            medicamento.Presentacion = oMedicamentoCLS.presentacion;
                            medicamento.Bhabilitado = 1;
                            bd.Medicamento.Add(medicamento);
                            bd.SaveChanges();
                        }
                        else
                        {
                            //verifica si el id tiene un valor edita
                            Medicamento medicamento = bd.Medicamento.
                                Where(p => p.Iidmedicamento == oMedicamentoCLS.iidMedicamento)
                                .First();

                            //modifica los datos que fueron recibidos en el modelo
                            medicamento.Nombre = oMedicamentoCLS.nombre;
                            medicamento.Concentracion = oMedicamentoCLS.concentracion;
                            medicamento.Iidformafarmaceutica = oMedicamentoCLS.iidFormaFarmaceutica;
                            medicamento.Precio = oMedicamentoCLS.precio;
                            medicamento.Stock = oMedicamentoCLS.stock;
                            medicamento.Presentacion = oMedicamentoCLS.presentacion;
                            bd.SaveChanges();
                        }

                    }

                }

            }
            catch (Exception)
            {

                return View(nombreVista, oMedicamentoCLS);
            }

            return RedirectToAction("Index");
        
        }
    }

}

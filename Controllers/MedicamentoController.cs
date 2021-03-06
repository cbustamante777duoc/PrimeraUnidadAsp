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
                if (oMedicamentoCLS.iidFormaFarmaceutica == 0)
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
    }

}

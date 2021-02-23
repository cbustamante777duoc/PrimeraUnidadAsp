using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            List<MedicamentoCLS> listaMedicamento = new List<MedicamentoCLS>();
            using (BDHospitalContext bd = new BDHospitalContext()) 
            {
                listaMedicamento = (from medicamento in bd.Medicamento
                                    join formaFarmaceutica in bd.FormaFarmaceutica
                                    on medicamento.Iidformafarmaceutica equals
                                    formaFarmaceutica.Iidformafarmaceutica
                                    select new MedicamentoCLS
                                    {
                                        iidMedicamento = medicamento.Iidmedicamento,
                                        nombre = medicamento.Nombre,
                                        precio = (decimal)medicamento.Precio,
                                        stock = (int)medicamento.Stock,
                                        nombreFormaFarmaceutica = formaFarmaceutica.Nombre
                                    }).ToList();
            
            }
                return View(listaMedicamento);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Clases
{
    public class MedicamentoCLS
    {
        [Display(Name = "Id del medicamento")]
        public int iidMedicamento { get; set; }

        [Display(Name = "Nombre del medicamento")]
        public string nombre { get; set; }
        [Display(Name = "Precio")]
        public decimal precio { get; set; }
        [Display(Name = "Stock")]
        public int stock { get; set; }
        [Display(Name = "Nombre de la Forma Farmaceutica")]
        public string nombreFormaFarmaceutica { get; set; }


        public int iidFormaFarmaceutica { get; set; }

    }
}

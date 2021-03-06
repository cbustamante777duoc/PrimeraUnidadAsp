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
        [Required(ErrorMessage ="ingrese nombre del medicamento")]
        public string nombre { get; set; }
        
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "ingrese precio del medicamento")]
        public decimal precio { get; set; }

        [Display(Name = "Stock")]
        [Required(ErrorMessage = "ingrese el stock medicamento")]
        public int stock { get; set; }
        [Display(Name = "Nombre de la Forma Farmaceutica")]
        public string nombreFormaFarmaceutica { get; set; }

        [Display(Name = "concentracion")]
        public string concentracion { get; set; }

        [Display(Name = "presentacion")]
        public string presentacion { get; set; }

        [Display(Name = "selecione forma farmaceutica ")]
        [Required(ErrorMessage = "ingrese la forma farmaceutica")]
        public int iidFormaFarmaceutica { get; set; }

    }
}

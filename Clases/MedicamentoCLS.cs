using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Clases
{
    public class MedicamentoCLS
    {
        [Display(Name = "Id del medicamento")]
        [DisplayName("Id del medicamento")]
        public int iidMedicamento { get; set; }

        [Display(Name = "Nombre del medicamento")]
        [DisplayName("nombre del medicamento")]
        [Required(ErrorMessage ="ingrese nombre del medicamento")]
        public string nombre { get; set; }
        
        [Display(Name = "Precio")]
        [DisplayName("Precio del medicamento")]
        [Required(ErrorMessage = "ingrese precio del medicamento")]
        public decimal? precio { get; set; }

        [Display(Name = "Stock")]
        [DisplayName("stock")]
        [Required(ErrorMessage = "ingrese el stock medicamento")]
        [Range (0,10000, ErrorMessage="el stock debe estar en el rango de 0 a 10000")]
        public int? stock { get; set; }

        [Display(Name = "Nombre de la Forma Farmaceutica")]
        [DisplayName("nombre forma famaceutica")]
        public string nombreFormaFarmaceutica { get; set; }

        [Display(Name = "concentracion")]
        public string concentracion { get; set; }

        [Display(Name = "presentacion")]
        public string presentacion { get; set; }

        [Display(Name = "selecione forma farmaceutica ")]
        [Required(ErrorMessage = "ingrese la forma farmaceutica")]
        public int? iidFormaFarmaceutica { get; set; }

    }
}

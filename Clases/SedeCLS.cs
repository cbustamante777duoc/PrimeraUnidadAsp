using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Clases
{
    public class SedeCLS
    {
        [Display(Name ="Id sede")]
        [DisplayName("Id de la sede")]
        public int iidSede { get; set; }

        [Display(Name = "Nombre de la sede")]
        [DisplayName("Nombre de la sede")]
        [Required(ErrorMessage ="ingrese el nombre de la sede")]
        public string nombreSede { get; set; }
        [Display(Name = "Direccion")]
        [DisplayName("Direccion  de la sede")]
        [Required(ErrorMessage = "ingrese el direcion de la sede")]
        public string direcion { get; set; }
    }
}

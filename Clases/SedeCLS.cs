using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Clases
{
    public class SedeCLS
    {
        [Display(Name ="Id sede")]
        public int iidSede { get; set; }

        [Display(Name = "Nombre de la sede")]
        public string nombreSede { get; set; }
        [Display(Name = "Direccion")]
        public string direcion { get; set; }
    }
}

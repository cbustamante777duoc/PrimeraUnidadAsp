using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Clases
{
    public class EspecialidadCLS
    {
        [Display(Name = "Id especialidad")]
        
        public int iidespecilidad { get; set; }
        [Display(Name = "Nombre especialidad")]
        public string nombre { get; set; }
        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Clases
{
    public class TipoUsuarioCLS
    {
        [Display(Name ="id tipo usuario")]
        public int iidTipoUsuario { get; set; }

        [Display(Name = "nombre del usuario")]
        public string nombre { get; set; }

        [Display(Name = "descripcion")]
        public string descripcion { get; set; }

        
    }
}

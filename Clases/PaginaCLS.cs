using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Clases
{
    public class PaginaCLS
    {
        [Display(Name ="Id de la pagina")]
        public int iidPagina { get; set; }

        [Display(Name = "Mensaje")]
        public string mensaje { get; set; }
        [Display(Name = "Nombre de la accion")]
        public string accion { get; set; }
        [Display(Name = "Nombre del controlador")]
        public string controlador { get; set; }
    }
}

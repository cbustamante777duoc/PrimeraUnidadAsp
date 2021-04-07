using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Clases
{
    public class PaginaCLS
    {
        [Display(Name ="Id de la pagina")]
        [DisplayName("id de la pagina")]
        public int iidPagina { get; set; }

        [Display(Name = "Mensaje")]
        [DisplayName("mensaje de la pagina")]
        [Required(ErrorMessage ="Debe ingresar Mensaje")]
        public string mensaje { get; set; }

        [Display(Name = "Nombre de la accion")]
        [DisplayName("accion de la pagina")]
        [Required(ErrorMessage = "Debe ingresar Acccion")]
        public string accion { get; set; }

        [Display(Name = "Nombre del controlador")]
        [DisplayName("controlador de la pagina")]
        [Required(ErrorMessage = "Debe ingresar el nombre del controlador")]
        [MinLength(3,ErrorMessage = "la logitud minima es de 3 caracteres")]
        [MaxLength(100, ErrorMessage = "la logitud maxima de 100 caracteres")]
        public string controlador { get; set; }

        public string mensajeError { get; set; }
    }
}
 
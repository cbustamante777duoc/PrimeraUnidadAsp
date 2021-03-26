using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Clases
{
    public class TipoUsuarioCLS
    {
        [Display(Name = "id tipo usuario")]
        public int iidTipoUsuario { get; set; }

        [Required(ErrorMessage = "ingrese nombre del  tipo usuario")]
        [Display(Name = "nombre del usuario")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "ingrese la descripcion del tipo usuario")]
        [Display(Name = "descripcion")]
        public string descripcion { get; set; }


        //validacion para los valores repetidos en la bd
        public string mensajeErrorNombre { get; set; }

        public string mensajeErrorDescripcion { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Clases
{
    public class EspecialidadCLS
    {
        //[DisplayName = para el REPORTE]
        //Display = PARA EL FORMULARIO

        [Display(Name = "Id especialidad")]
        [DisplayName("Id de la especialidad")]
        public int iidespecilidad { get; set; }

        [Display(Name = "Nombre especialidad")]
        [DisplayName("nombre de la  especialidad")]
        [Required(ErrorMessage ="Ingrese el nombre de la especialidad")]
        public string nombre { get; set; }

        [Display(Name = "Descripcion")]
        [DisplayName("Descripcion de la  especialidad")]
        [Required(ErrorMessage = "Ingrese la descripcion de la especialidad")]
        public string descripcion { get; set; }

        public string mensajeError { get; set; }
    }
}

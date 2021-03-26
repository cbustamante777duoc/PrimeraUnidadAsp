using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAppNetCore.Clases
{
    public class PersonaCLS
    {
        [Display(Name = "Id persona")]
        public int iidPersona { get; set; }

        //añadiendo mas propiedades

        [Display(Name = " nombre")]
        [Required(ErrorMessage = "Ingrese el nombre")]
        public string nombre { get; set; }

        [Display(Name = "apellido paterno")]
        [Required(ErrorMessage = "Ingrese el apellido paterno")]
        public string apPaterno { get; set; }

        [Display(Name = "apellido materno")]
        [Required(ErrorMessage = "Ingrese el apellido materno")]
        public string apMaterno { get; set; }


        [Display(Name = "telefono fijo")]
        [Required(ErrorMessage = "Ingrese el telefono fijo")]
        [MinLength(8,ErrorMessage ="longitud minima 8 caracteres")]
        public string telefonoFijo { get; set; }

        [Display(Name = "telefono celular")]
        public string telefonoCelular { get; set; }

        [DataType(DataType.Date,ErrorMessage ="el formato de fecha no es correcto")]
        [Display(Name ="fecha de nacimiento")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [Required(ErrorMessage ="ingrese la fecha de nacimiento")]
        public DateTime? fechaNacimiento { get; set; }

        //nombre completo solamente la uso para listar en el contralodor
        [Display(Name = "Nombre completo")]
        public string nombreCompleto { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress,ErrorMessage ="ingrese un correo valido")]
        public string email { get; set; }

        [Display(Name = "Genero")]
        public string nombreSexo { get; set; }

        [Display(Name ="selecione una opcion")]
        [Required(ErrorMessage ="seleccione sexo")]
        
        public int? iidSexo { get; set; }


        public string mensajeError { get; set; }
    }
}

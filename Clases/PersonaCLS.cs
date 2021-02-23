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
        [Display(Name = "Nombre completo")]
        public string nombreCompleto { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Genero")]
        public string nombreSexo { get; set; }

    }
}

using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiPrimeraAppNetCore.Clases;

namespace MiPrimeraAppNetCore.Controllers
{
    public class PersonaController : Controller
    {
        public IActionResult Index()
        {
            List<PersonaCLS> listaPersona = new List<PersonaCLS>();
            using (BDHospitalContext db = new BDHospitalContext()) 
            {
                listaPersona = (from persona in db.Persona
                                join sexo in db.Sexo
                                on persona.Iidsexo equals sexo.Iidsexo
                                where persona.Bhabilitado == 1
                                select new PersonaCLS
                                {
                                    iidPersona = persona.Iidpersona,
                                    nombreCompleto = persona.Nombre + " " + persona.Appaterno +
                                                    " " + persona.Apmaterno,
                                    email = persona.Email,
                                    nombreSexo = sexo.Nombre
                                }).ToList();
           }
             return View(listaPersona);
        }
    }
}

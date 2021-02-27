using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiPrimeraAppNetCore.Clases;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiPrimeraAppNetCore.Controllers
{
    public class PersonaController : Controller
    {

        public void LlenarSexo() 
        {
            //para llenar el comboBox se necesita SelectListItem
            List<SelectListItem> listaSexo = new List<SelectListItem>();
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                listaSexo = (from sexo in bd.Sexo
                             where sexo.Bhabilitado == 1
                             select new SelectListItem
                             {
                                 Text = sexo.Nombre,
                                 Value = sexo.Iidsexo.ToString()
                             }).ToList();
            }
            ViewBag.listaSexo = listaSexo;
            //agregando un nuevo item a la lista
            listaSexo.Insert(0, new SelectListItem
            {
                Text = "--Seleccione--",
                Value = ""
            });
        }

        public IActionResult Index(PersonaCLS oPersonaCLS)
        {
            List<PersonaCLS> listaPersona = new List<PersonaCLS>();
            LlenarSexo();
            using (BDHospitalContext db = new BDHospitalContext()) 
            {
                //si la lista esta vacia
                if (oPersonaCLS.iidSexo == 0)
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
                else
                {
                    listaPersona = (from persona in db.Persona
                                    join sexo in db.Sexo
                                    on persona.Iidsexo equals sexo.Iidsexo
                                    where persona.Bhabilitado == 1
                                    && persona.Iidsexo == oPersonaCLS.iidSexo
                                    select new PersonaCLS
                                    {
                                        iidPersona = persona.Iidpersona,
                                        nombreCompleto = persona.Nombre + " " + persona.Appaterno +
                                                        " " + persona.Apmaterno,
                                        email = persona.Email,
                                        nombreSexo = sexo.Nombre
                                    }).ToList();

                }
           }
             return View(listaPersona);
        }
    }
}

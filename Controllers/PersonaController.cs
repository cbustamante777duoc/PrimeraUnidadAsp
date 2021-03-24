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
                if (oPersonaCLS.iidSexo == 0 || oPersonaCLS.iidSexo==null)
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

        public IActionResult Agregar() 
        {
            LlenarSexo();
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(PersonaCLS oPersonaCLS)
        {
            LlenarSexo();
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oPersonaCLS);
                }
                else
                {
                    using (BDHospitalContext bd = new BDHospitalContext())
                    {
                        Persona persona = new Persona();
                        persona.Nombre = oPersonaCLS.nombre;
                        persona.Appaterno = oPersonaCLS.apPaterno;
                        persona.Apmaterno = oPersonaCLS.apMaterno;
                        persona.Telefonofijo = oPersonaCLS.telefonoFijo;
                        persona.Telefonocelular = oPersonaCLS.telefonoCelular;
                        persona.Fechanacimiento= oPersonaCLS.fechaNacimiento;
                        persona.Email= oPersonaCLS.email;
                        persona.Iidsexo = oPersonaCLS.iidSexo;
                        persona.Bhabilitado = 1;
                        bd.Add(persona);
                        bd.SaveChanges();


                    }
                }
                
            }
            catch (Exception)
            {

                return View(oPersonaCLS);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int iidPersona) 
        {
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                Persona persona = bd.Persona.Where(p => p.Iidpersona == iidPersona).First();
               
                persona.Bhabilitado = 0;
                bd.SaveChanges();
            }

            return RedirectToAction("Index");
            
        }


    }
}

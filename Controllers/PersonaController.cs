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
                if (oPersonaCLS.iidSexo == 0 || oPersonaCLS.iidSexo == null)
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
        public IActionResult Guardar(PersonaCLS oPersonaCLS)
        {
            //llenado del combobox
            LlenarSexo();

            string nombreVista = "";
            int numeroVeces = 0;

            //validacion de las vista
            if (oPersonaCLS.iidPersona == 0) nombreVista = "Agregar";
            else nombreVista = "Editar";

            try
            {
                using (BDHospitalContext bd = new BDHospitalContext())
                {
                    //validacion por repeticion en la bd
                    //para ellos se ven los espacios vacios y pasarlo a mayuscula
                    //Agregar
                    oPersonaCLS.nombreCompleto = oPersonaCLS.nombre.Trim().ToUpper() + " " + oPersonaCLS.apPaterno.Trim().ToUpper() 
                        + " " + oPersonaCLS.apMaterno.Trim().ToUpper();
                    if (oPersonaCLS.iidPersona == 0)
                    {
                        numeroVeces = bd.Persona
                            .Where(p => p.Nombre.Trim().ToUpper() + " " + p.Appaterno.Trim().ToUpper() + " "
                            + p.Apmaterno.Trim().ToUpper() == oPersonaCLS.nombreCompleto)
                            .Count();

                    }
                    else
                    {
                        //VALIDACION PARA EDITAR QUE NO SE REPITA EN BD
                        bd.Persona
                           .Where(p => p.Nombre.Trim().ToUpper() + " " + p.Appaterno.Trim().ToUpper() + " "
                           + p.Apmaterno.Trim().ToUpper() == oPersonaCLS.nombreCompleto
                           && p.Iidpersona != oPersonaCLS.iidPersona
                           ).Count();

                    }

                    //validacion del modelo o numero de veces que se repite en la bd
                    if (!ModelState.IsValid || numeroVeces>=0)
                    {
                        if (numeroVeces >= 1) oPersonaCLS.mensajeError = "la persona ya existe";
                        return View(nombreVista, oPersonaCLS);
                    }

                    else
                    {
                        Persona persona = new Persona();
                        persona.Nombre = oPersonaCLS.nombre;
                        persona.Appaterno = oPersonaCLS.apPaterno;
                        persona.Apmaterno = oPersonaCLS.apMaterno;
                        persona.Telefonofijo = oPersonaCLS.telefonoFijo;
                        persona.Telefonocelular = oPersonaCLS.telefonoCelular;
                        persona.Fechanacimiento = oPersonaCLS.fechaNacimiento;
                        persona.Email = oPersonaCLS.email;
                        persona.Iidsexo = oPersonaCLS.iidSexo;
                        persona.Bhabilitado = 1;
                        bd.Add(persona);
                        bd.SaveChanges();


                    }
                }


            }
            catch (Exception)
            {

                return View(nombreVista, oPersonaCLS);
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

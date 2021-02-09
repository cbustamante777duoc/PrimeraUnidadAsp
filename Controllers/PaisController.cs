using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppNetCore.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MiPrimeraAppNetCore.Controllers
{
    public class PaisController : Controller
    {
        public IActionResult Inicio()
        {
            return View();
        }

        public IActionResult Lista()
        {
            return View();
        }

        public IActionResult PruebaLista()
        {
            List<InstructorCLS> lista = new List<InstructorCLS>();
            InstructorCLS oInstructorCLS = new InstructorCLS();
            oInstructorCLS.nombre = "Cristian";
            oInstructorCLS.apellido = "Bustamante";
            oInstructorCLS.SegundoApellido = "Araya";
            lista.Add(oInstructorCLS);
            oInstructorCLS = new InstructorCLS();
            oInstructorCLS.nombre = "jennie";
            oInstructorCLS.apellido = "kim";
            oInstructorCLS.SegundoApellido = "Cheongdam-dong";
            lista.Add(oInstructorCLS);

            return View(lista);
        }
        public double sueldo()
        {
            return 1000.5;
        }

        public string saludo(InstructorCLS oInstructorCLS) 
        {
            return "hola " + oInstructorCLS.nombre + " "+ oInstructorCLS.apellido+ " "+ oInstructorCLS.SegundoApellido;
        }

        public InstructorCLS mostrarInstructor() 
        {
            InstructorCLS oInstructorCLS = new InstructorCLS();
            oInstructorCLS.nombre = "Cristian";
            oInstructorCLS.apellido = "Bustamante";
            oInstructorCLS.SegundoApellido = "Araya";
            return oInstructorCLS;
        }

        public List<InstructorCLS> mostrarListaInstructor()
        {
            List<InstructorCLS> lista = new List<InstructorCLS>();
            InstructorCLS oInstructorCLS = new InstructorCLS();
            oInstructorCLS.nombre = "Cristian";
            oInstructorCLS.apellido = "Bustamante";
            oInstructorCLS.SegundoApellido = "Araya";
            lista.Add(oInstructorCLS);
            oInstructorCLS = new InstructorCLS();
            oInstructorCLS.nombre = "jennie";
            oInstructorCLS.apellido = "kim";
            oInstructorCLS.SegundoApellido = "Cheongdam-dong";
            lista.Add(oInstructorCLS);
            return lista;
        }
    }
}

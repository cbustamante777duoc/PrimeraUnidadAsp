using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppNetCore.Clases;
using MiPrimeraAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using cm = System.ComponentModel;


namespace MiPrimeraAppNetCore.Controllers
{
    public class BaseController : Controller
    {
        //metodo que genera un array de bytes
        public byte[] exportarExcelDatos<T>(string[] nombrePropiedades, List<T> lista)
        {

            using (MemoryStream ms = new MemoryStream())
            {
                //uso de excel no comercial
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (ExcelPackage ep = new ExcelPackage())
                {
                    ep.Workbook.Worksheets.Add("Hoja");
                    //hace referencia a la pagina de arriba
                    ExcelWorksheet ew = ep.Workbook.Worksheets[0];

                    //se obtiene el DISPLAYNAME de la clase
                    //cm lo uso del using para referenciar
                    Dictionary<string, string> diccionario = cm.TypeDescriptor.GetProperties(typeof(T)).Cast<cm.PropertyDescriptor>()
                        .ToDictionary(p => p.Name, p => p.DisplayName);

                    for (int i = 0; i < nombrePropiedades.Length; i++)
                    {
                        //llenado con las cabeceras
                        ew.Cells[1, i + 1].Value = diccionario[nombrePropiedades[i]];
                        //definiendo el ancho
                        ew.Column(i + 1).Width = 50;

                    }
                    int fila = 2;
                    int columna = 1;

                    foreach (object item in lista)
                    {
                        //llenado del contenido del excel
                        columna = 1;
                        foreach (string propiedad in nombrePropiedades)
                        {
                            ew.Cells[fila, columna].Value =
                                item.GetType().GetProperty(propiedad).GetValue(item).ToString();

                            columna++;
                        }

                        fila++;
                    }

                    //excelPackage se guarda en memoryStream
                    ep.SaveAs(ms);

                    byte[] buffer = ms.ToArray();

                    return buffer;
                }
            }

        }
    }
}

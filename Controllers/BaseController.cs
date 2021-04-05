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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;



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

        public byte[] exportarPDFDatos<T>(string[] nombrePropiedades, List<T> lista)
        {
            //se obtiene el DISPLAYNAME de la clase
            //cm lo uso del using para referenciar
            Dictionary<string, string> diccionario = cm.TypeDescriptor.GetProperties(typeof(T)).Cast<cm.PropertyDescriptor>()
                .ToDictionary(p => p.Name, p => p.DisplayName);

            using (MemoryStream ms = new MemoryStream())
            {
                //el pdfWriter se enlasa en memoryStream
                PdfWriter writer = new PdfWriter(ms);
                //pdfDocument se le pasa en writer
                using (var pdfDoc = new PdfDocument(writer))
                {
                    //se crea un nuevo documento
                    Document doc = new Document(pdfDoc);
                    //titulo
                    Paragraph c1 = new Paragraph("Reporte en PDF");
                    //tamanio de la letra
                    c1.SetFontSize(20);
                    //se centra el titulo
                    c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    //se agrega el titulo al PDF
                    doc.Add(c1);

                    //se crea un tabla con el numero de columnas
                    Table table = new Table(nombrePropiedades.Length);
                    Cell celda;

                    for (int i = 0; i < nombrePropiedades.Length; i++)
                    {
                        //instacia de la celda
                        celda = new Cell();
                        //llenado de la tabla con los nombres de las cabezeras
                        celda.Add(new Paragraph(diccionario[nombrePropiedades[i]]));
                        //se pasa la celda a la tabla
                        table.AddHeaderCell(celda);


                    }

                    foreach (object item in lista)
                    {
                        foreach (string propiedad in nombrePropiedades)
                        {
                            //instacia de la celda
                            celda = new Cell();
                            //se llena el contenido a la tabla
                            celda.Add(new Paragraph(
                                  item.GetType().GetProperty(propiedad).GetValue(item).ToString()
                                ));
                            //se añade la celda a la tabla
                            table.AddCell(celda);

                        }
                    }

                    //se agrega la tabla al documento
                    doc.Add(table);

                    //se cierra el documento
                    doc.Close();
                    writer.Close();
                }

                return ms.ToArray();
            }
        }
    }
}

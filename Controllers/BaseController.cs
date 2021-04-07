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
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;




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

                    if (nombrePropiedades != null)
                    {
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
                    }
                    //se cierra el documento
                    doc.Close();
                    writer.Close();
                }

                return ms.ToArray();
            }
        }

        public byte[] exportarDatosWord<T>(string[] nombrePropiedades, List<T> lista)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                WordDocument document = new WordDocument();
                //agregar una secion
                WSection section = document.AddSection() as WSection;
                //agregando un margen
                section.PageSetup.Margins.All = 72;
                //tamanio
                section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);
                //agregando un parrafo
                IWParagraph paragraph = section.AddParagraph();
                //tipo de letra
                paragraph.ApplyStyle("Normal");
                //centrar
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
                //agregando un texto
                WTextRange textRange = paragraph.AppendText("Reporte en Word") as WTextRange;
                //el texto va a tener un tamañio de 20f
                textRange.CharacterFormat.FontSize = 20f;
                //tipo de letra
                textRange.CharacterFormat.FontName = "Calibri";
                //color
                textRange.CharacterFormat.TextColor = Syncfusion.Drawing.Color.Blue;

                //agregando una tabla
                if (nombrePropiedades != null)
                {
                    IWTable table = section.AddTable();
                    //numero de columnas
                    int numeroColumnas = nombrePropiedades.Length;
                    //numero de filas
                    int nfilas = lista.Count;
                    //agregando las columnas y filas de la tabla va a tener
                    table.ResetCells(nfilas + 1, numeroColumnas);
                    //displayName
                    Dictionary<string, string> diccionario = cm.TypeDescriptor.GetProperties(typeof(T)).Cast<cm.PropertyDescriptor>()
                          .ToDictionary(p => p.Name, p => p.DisplayName);
                    if (nombrePropiedades != null)
                    {

                        for (int i = 0; i < numeroColumnas; i++)
                        {
                            //insertando las cabeceras
                            table[0, i].AddParagraph().AppendText(diccionario[nombrePropiedades[i]]);

                        }
                        int fila = 1;
                        int col = 0;

                        foreach (object item in lista)
                        {
                            col = 0;
                            foreach (string propiedad in nombrePropiedades)
                            {
                                table[fila, col].AddParagraph().AppendText(
                                    item.GetType().GetProperty(propiedad).GetValue(item).ToString());
                                col++;
                            }
                            fila++;

                        }
                    }
                }
                //guardo en documento
                document.Save(ms, FormatType.Docx);

                return ms.ToArray();

            }

        }
    }
}

using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AcuseReciboSAPController : Controller
    {
        // GET: AcuseReciboSAP
        [HttpGet]
        public ActionResult GetReport()
        {
            string Fechabusqueda = null;
            string NombreBusqueda = null;
            string FechaB = null;

            ML.AcuRe acuRe = new ML.AcuRe();

            ML.Result result = BL.Acuse.GetReport(Fechabusqueda, NombreBusqueda, FechaB);

            acuRe.AcureS = result.Objects;

            return View(acuRe);
        }

        [HttpPost]
        public ActionResult GetReport(string Fechabusqueda, string NombreBusqueda, string FechaB)
        {
            ML.AcuRe acuRe = new ML.AcuRe();

            ML.Result result = BL.Acuse.GetReport(Fechabusqueda, NombreBusqueda, FechaB);

            acuRe.AcureS = result.Objects;

            return View(acuRe);
        }
        [HttpPost]
        public ActionResult DownloadExcel(string Fechabusqueda, string NombreBusqueda, string FechaB)
        {
            ML.Result result = BL.Acuse.GetReport(Fechabusqueda, NombreBusqueda, FechaB);

            if (result.Objects is IEnumerable<object> objectList)
            {
                var acuReList = ConvertToAcuReList(objectList);

                byte[] fileContents = GenerateExcel(acuReList);

                // Cambia el tipo MIME a "application/octet-stream"
                return File(fileContents, "application/octet-stream", "AcuseReciboSAP.xlsx");
            }
            else
            {
                TempData["Message"] = "Error al obtener datos para descargar Excel.";
                return RedirectToAction("GetReport");
            }
        }

        private List<ML.AcuRe> ConvertToAcuReList(IEnumerable<object> objects)
        {
            List<ML.AcuRe> acuReList = new List<ML.AcuRe>();

            foreach (var obj in objects)
            {
                if (obj is ML.AcuRe acuRe)
                {
                    acuReList.Add(acuRe);
                }
            }

            return acuReList;
        }

        private byte[] GenerateExcel(List<ML.AcuRe> acuseList)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AcuseReciboSAP");

                // Encabezados
                worksheet.Cell(1, 1).Value = "Company";
                worksheet.Cell(1, 2).Value = "Cliente";
                worksheet.Cell(1, 3).Value = "Ruta Original";
                worksheet.Cell(1, 4).Value = "Nombre del Archivo";
                worksheet.Cell(1, 5).Value = "Ruta SAP";
                worksheet.Cell(1, 6).Value = "Status";
                worksheet.Cell(1, 7).Value = "Fecha de carga";

                // Datos
                int row = 2;
                foreach (var acu in acuseList)
                {
                    worksheet.Cell(row, 1).Value = acu.SegCompany;
                    worksheet.Cell(row, 2).Value = acu.SegClient;
                    worksheet.Cell(row, 3).Value = acu.RouteFileOriginal;
                    worksheet.Cell(row, 4).Value = acu.FileNameComplete;
                    worksheet.Cell(row, 5).Value = acu.RouteFileSape;
                    worksheet.Cell(row, 6).Value = acu.Status;
                    worksheet.Cell(row, 7).Value = acu.CreationDate;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }



    }
}
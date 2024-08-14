using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using celsia_assetsment_johan_arboleda.App.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace celsia_assetsment_johan_arboleda.Controllers.ManagementExcel
{
    [Route("[controller]")]
    public class ImportExcelController : Controller
    {
        private readonly IExcel _excelService;

        public ImportExcelController(IExcel excelService)
        {
            _excelService = excelService;
        }

        [HttpPost]
        public async Task<IActionResult> ImportData(IFormFile file)
        {
            // Verificar si el archivo es nulo o tiene un tamaño menor o igual a 0
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Por favor, sube un archivo válido.";
                return View("Inscriptions", "Home");
            }

            // Verificar el tipo de archivo
            var allowedExtensions = new[] { ".xlsx" };
            try
            {
                using (var stream = file.OpenReadStream())
                {
                    await _excelService.ImportDataAsync(stream);
                    TempData["SuccessMessage"] = "Datos importados correctamente.";
                }
            }
            catch (ApplicationException ex)
            {
                TempData["ErrorMessage"] = $"Ha ocurrido un error al importar los datos: {ex.Message}";
                return View("Inscriptions", "Home");
            }
            return RedirectToAction("Inscriptions", "Home");
        }
    }
}

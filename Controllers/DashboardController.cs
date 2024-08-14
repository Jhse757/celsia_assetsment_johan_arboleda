using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using celsia_assetsment_johan_arboleda.Infraestructure.Context;
using celsia_assetsment_johan_arboleda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace celsia_assetsment_johan_arboleda.Controllers
{
    [Route("Dashboard")]
    public class DashboardController : Controller
    {

        // Inyección de la base del ManagementContext y del Logger
        private readonly ManagementContext _context;
        private readonly ILogger<DashboardController> _logger;
        public DashboardController(ManagementContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
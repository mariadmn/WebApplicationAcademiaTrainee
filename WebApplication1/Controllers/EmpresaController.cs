using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly ILogger<EmpresaController> _logger;

        public EmpresaController(ILogger<EmpresaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

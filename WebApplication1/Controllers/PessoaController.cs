using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class PessoaController : Controller
    {
        private readonly ILogger<PessoaController> _logger;

        public PessoaController(ILogger<PessoaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /*public ActionResult Create([Bind("Codigo,Nome")] PessoaModel pessoaModel)
        {
            return View("Index");
        }*/
    }
}

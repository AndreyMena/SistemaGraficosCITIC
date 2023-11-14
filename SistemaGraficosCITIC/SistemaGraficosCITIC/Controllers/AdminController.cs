using Microsoft.AspNetCore.Mvc;

namespace SistemaGraficosCITIC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

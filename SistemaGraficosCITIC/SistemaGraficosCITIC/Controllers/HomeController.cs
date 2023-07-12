using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Models;
using System.Diagnostics;

namespace SistemaGraficosCITIC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor of the projectsController class
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// GET Index method for home controller
        /// </summary>
        /// <returns>The Task of action to the view</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET Privace method for home controller
        /// </summary>
        /// <returns>The Task of action to the view</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// GET responso method for errors in home controller
        /// </summary>
        /// <returns>The Task of action to the view</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
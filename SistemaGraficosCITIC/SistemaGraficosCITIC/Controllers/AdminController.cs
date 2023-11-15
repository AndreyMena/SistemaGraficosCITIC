using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Repositories;

namespace SistemaGraficosCITIC.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(SistemaGraficosCITICContext context, SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> _userManager, IProjectRepository _projectRepository) : base(context, _signInManager, _userManager, _projectRepository)
        {
        }

        /// <summary>
        /// Index GET method
        /// </summary>
        /// <returns>The Task of action to the view</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return _context.Publication != null ?
                        View(await _context.Project.ToListAsync()) :
                        Problem("Entity set 'SistemaGraficosCITICContext.Publication' is null.");
        }

        /// <summary>
        /// Index GET method
        /// </summary>
        /// <returns>The Task of action to the view</returns>
        [HttpGet]
        public async Task<IActionResult> Graphics()
        {
            return View();
        }

        /// <summary>
        /// Index GET method
        /// </summary>
        /// <returns>The Task of action to the view</returns>
        [HttpGet]
        public async Task<IActionResult> Configurations()
        {
            return View();
        }
    }
}

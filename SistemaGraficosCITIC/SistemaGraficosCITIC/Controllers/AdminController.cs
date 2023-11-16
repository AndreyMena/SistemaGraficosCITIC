using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Models.Domain;
using SistemaGraficosCITIC.Repositories;
using SistemaGraficosCITIC.Views.ViewModels;

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

        [HttpGet]
        public async Task<IActionResult> ResearcherTypes()
        {
            var researcherTypes = await _context.ResearcherTypes.ToListAsync();

            if (researcherTypes == null)
            {
                return NotFound();
            }
            return View(researcherTypes);
        }

        [HttpPost]
        public async Task<IActionResult> ResearcherTypes(ResearcherTypeModel model, int? check)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var userName = User.Identity!.Name;
                    var currentUser = await userManager.FindByNameAsync(userName);

                    var researcherType = new ResearcherTypes(
                        model.ResearcherTypeName
                    );
                    _context.ResearcherTypes.Add(researcherType);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction("ResearcherTypes", "Admin");
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteType(int id)
        {
            var researcherType = await _context.ResearcherTypes.FindAsync(id);
            if (researcherType != null)
            {
                _context.ResearcherTypes.Remove(researcherType);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ResearcherTypes", "Admin");
        }
    }
}

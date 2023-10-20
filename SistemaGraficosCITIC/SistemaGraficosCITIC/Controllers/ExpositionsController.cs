using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Models.Domain;
using SistemaGraficosCITIC.Repositories;
using SistemaGraficosCITIC.Views.ViewModels;

namespace SistemaGraficosCITIC.Controllers
{
    public class ExpositionsController : Controller
    {
        private readonly SistemaGraficosCITICContext _context;

        private readonly UserManager<IdentityUser> userManager;

        private readonly SignInManager<IdentityUser> signInManager;

        private readonly IProjectRepository projectRepository;

        /// <summary>
        /// Constructor of the ExpositionsController class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_signInManager"></param>
        /// <param name="_userManager"></param>
        /// <param name="_projectRepository"></param>
        public ExpositionsController(SistemaGraficosCITICContext context, SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager, IProjectRepository _projectRepository)
        {
            _context = context;
            userManager = _userManager;
            signInManager = _signInManager;
            projectRepository = _projectRepository;
        }

        /// <summary>
        /// GET method for see the index of expositions
        /// </summary>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Index()
        {
              return _context.Exposition != null ? 
                          View(await _context.Exposition.ToListAsync()) :
                          Problem("Entity set 'SistemaGraficosCITICContext.Exposition'  is null.");
        }

        /// <summary>
        /// GET method for see the details of expositions
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Exposition == null)
            {
                return NotFound();
            }

            var exposition = await _context.Exposition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exposition == null)
            {
                return NotFound();
            }

            return View(exposition);
        }

        /// <summary>
        /// GET method for create a expositions
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>The Task of action to the view</returns>e
        public async Task<IActionResult> Create(Guid projectId)
        {
            ViewData["projectId"] = projectId;
            var project = await projectRepository.GetAsync(projectId);
            ViewData["projectName"] = project?.Name;
            return View();
        }

        /// <summary>
        /// POST method for create a exposition and continue
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContinue(ExpositionModel model)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var userName = User.Identity!.Name;
                    var currentUser = await userManager.FindByNameAsync(userName);

                    var expo = new Exposition(model.ExpositionTitle!, model.ExpositionDate, model.ExpositionLocation!, model.ExpositionContext!,
                                                model.ExpositionParticipants!, model.ExpositionSpeaker!);
                    var project = await projectRepository.GetAsync(new Guid(model.ProjectId!));
                    _context.Exposition.Add(expo);
                    project!.Expositions.Add(expo);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                var projectId = model.ProjectId;
                return RedirectToAction("Create", "Products", new { projectId = projectId });
            }
            return View(model);
        }

        /// <summary>
        /// POST method for create a and create again a exposition
        /// </summary>
        /// <param name="model"></param>
        /// <param name="check"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpositionModel model, int? check)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var userName = User.Identity!.Name;
                    var currentUser = await userManager.FindByNameAsync(userName);

                    var expo = new Exposition(model.ExpositionTitle!, model.ExpositionDate, model.ExpositionLocation!, model.ExpositionContext!,
                                                model.ExpositionParticipants!, model.ExpositionSpeaker!);
                    var project = await projectRepository.GetAsync(new Guid(model.ProjectId!));
                    _context.Exposition.Add(expo);
                    project!.Expositions.Add(expo);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["projectId"] = model.ProjectId!;
                model.ExpositionTitle = "";
                model.ExpositionDate = new DateTime();
                model.ExpositionLocation = "";
                model.ExpositionContext = "";
                model.ExpositionParticipants = "";
                model.ExpositionSpeaker = "";
                return View("Create", new ExpositionModel());
            }
            return View(model);
        }

        /// <summary>
        /// GET method for edit a exposition
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Exposition == null)
            {
                return NotFound();
            }

            var exposition = await _context.Exposition.FindAsync(id);
            if (exposition == null)
            {
                return NotFound();
            }
            return View(exposition);
        }

        /// <summary>
        /// POST method for edit a expositions
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Date,Location,Context,Participants,Speaker")] Exposition exposition)
        {
            if (id != exposition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exposition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpositionExists(exposition.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Projects");
            }
            return View(exposition);
        }

        /// <summary>
        /// GET method for delete a expositions
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Exposition == null)
            {
                return NotFound();
            }

            var exposition = await _context.Exposition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exposition == null)
            {
                return NotFound();
            }

            return View(exposition);
        }

        /// <summary>
        /// POST method for delete a exposition
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_context.Exposition == null)
            {
                return Problem("Entity set 'SistemaGraficosCITICContext.Exposition'  is null.");
            }
            var exposition = await _context.Exposition.FindAsync(id);
            if (exposition != null)
            {
                _context.Exposition.Remove(exposition);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Projects");
        }

        /// <summary>
        /// Check of the exposition exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool tue if the product exists, false if not</returns>
        private bool ExpositionExists(Guid id)
        {
          return (_context.Exposition?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Skip method to go to the next page
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Skip(string projectId)
        {
            //var projectId = model.ProjectId;
            return RedirectToAction("Create", "Products", new { projectId = projectId });
        }

    }
}

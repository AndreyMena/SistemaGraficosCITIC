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
    public class PublicationsController : Controller
    {
        private readonly SistemaGraficosCITICContext _context;

        private readonly UserManager<IdentityUser> userManager;

        private readonly SignInManager<IdentityUser> signInManager;

        private readonly IProjectRepository projectRepository;

        /// <summary>
        /// Constructor for Publication
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_signInManager"></param>
        /// <param name="_userManager"></param>
        /// <param name="_projectRepository"></param>
        public PublicationsController(SistemaGraficosCITICContext context, SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager, IProjectRepository _projectRepository)
        {
            _context = context;
            userManager = _userManager;
            signInManager = _signInManager;
            projectRepository = _projectRepository;
        }

        /// <summary>
        /// Index GET method
        /// </summary>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Index()
        {

              return _context.Publication != null ? 
                          View(await _context.Publication.ToListAsync()) :
                          Problem("Entity set 'SistemaGraficosCITICContext.Publication'  is null.");
        }

        /// <summary>
        /// GET method for details of a publication
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Publication == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        /// <summary>
        /// GET method for create a publication
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>The Task of action to the view</returns>
        public IActionResult Create(string projectId)
        {
            //viewd
            ViewData["projectId"] = projectId;
            return View();
        }

        /// <summary>
        /// POST method for create a publication and continue
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContinue(PublicationModel model)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var userName = User.Identity!.Name;
                    var currentUser = await userManager.FindByNameAsync(userName);

                    var publication = new Publication(model.PublicationTitle!, model.PublicationDate, model.PublicationReference!, model.PublicationType!);
                    var project = await projectRepository.GetAsync(new Guid(model.ProjectId!));
                    _context.Publication.Add(publication);
                    project!.Publications.Add(publication);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                var projectId = model.ProjectId;
                return RedirectToAction("Create", "Expositions", new { projectId = projectId });
            }
            return View(model);
        }

        /// <summary>
        /// POST method for create a and create again
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="check"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PublicationModel model, int? check)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var userName = User.Identity!.Name;
                    var currentUser = await userManager.FindByNameAsync(userName);

                    var publication = new Publication(model.PublicationTitle!, model.PublicationDate, model.PublicationReference!, model.PublicationType!);
                    var project = await projectRepository.GetAsync(new Guid(model.ProjectId!));
                    _context.Publication.Add(publication);
                    project!.Publications.Add(publication);
                    await _context.SaveChangesAsync();
                    
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["projectId"] = model.ProjectId!;
                model.PublicationDate = new DateTime();
                model.PublicationTitle = "";
                model.PublicationType = "";
                model.PublicationReference = "";
                ///return View("Create", new PublicationModel());
                return RedirectToAction("Create", "Publications", new {projectId = model.ProjectId } );
            }
            return View(model);
        }

        /// <summary>
        /// GET method for edit a publication
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Publication == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }
            return View(publication);
        }

        /// <summary>
        /// POST method for edit a publication
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publication"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Date,Reference,Type")] Publication publication)
        {
            if (id != publication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicationExists(publication.Id))
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
            return View(publication);
        }

        /// <summary>
        /// GET method for delete a publication
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Publication == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        /// <summary>
        /// POST method for delete a publication
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_context.Publication == null)
            {
                return Problem("Entity set 'SistemaGraficosCITICContext.Publication'  is null.");
            }
            var publication = await _context.Publication.FindAsync(id);
            if (publication != null)
            {
                _context.Publication.Remove(publication);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Projects");
        }

        /// <summary>
        /// Check if the publication exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool true if the publication exists, false if not/returns>
        private bool PublicationExists(Guid id)
        {
          return (_context.Publication?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Skip method to go to the next page
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Skip(string projectId)
        {
            //var projectId = model.ProjectId;
            return RedirectToAction("Create", "Expositions", new { projectId = projectId });
        }

        public async Task<IActionResult> AddToProyect(PublicationModel model)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var userName = User.Identity!.Name;
                    var currentUser = await userManager.FindByNameAsync(userName);

                    var publication = new Publication(
                        model.PublicationTitle!,
                        model.PublicationDate,
                        model.PublicationReference!,
                        model.PublicationType!
                    );
                    var project = await projectRepository.GetAsync(new Guid(model.ProjectId!));
                    _context.Publication.Add(publication);
                    project!.Publications.Add(publication);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                var projectId = model.ProjectId;
                return RedirectToAction("Index", "Projects", new { projectId = projectId });
            }
            return View(model);
        }

    }
}

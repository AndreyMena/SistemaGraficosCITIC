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

        public ExpositionsController(SistemaGraficosCITICContext context, SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager, IProjectRepository _projectRepository)
        {
            _context = context;
            userManager = _userManager;
            signInManager = _signInManager;
            projectRepository = _projectRepository;
        }

        // GET: Expositions
        public async Task<IActionResult> Index()
        {
              return _context.Exposition != null ? 
                          View(await _context.Exposition.ToListAsync()) :
                          Problem("Entity set 'SistemaGraficosCITICContext.Exposition'  is null.");
        }

        // GET: Expositions/Details/5
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

        // GET: Expositions/Create
        public IActionResult Create(string projectId)
        {
            ViewData["projectId"] = projectId;
            return View();
        }

        // POST: Expositions/Create
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

                    var expo = new Exposition(model.ExpositionDate, model.ExpositionLocation, model.ExpositionContext);
                    var project = await projectRepository.GetAsync(new Guid(model.ProjectId));
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

        // POST: Expositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

                    var expo = new Exposition(model.ExpositionDate, model.ExpositionLocation, model.ExpositionContext);
                    var project = await projectRepository.GetAsync(new Guid(model.ProjectId));
                    _context.Exposition.Add(expo);
                    project!.Expositions.Add(expo);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["projectId"] = model.ProjectId!;
                model.ExpositionDate = new DateTime();
                model.ExpositionLocation = "";
                model.ExpositionContext = "";
                return View("Create", new ExpositionModel());
            }
            return View(model);
        }

        // GET: Expositions/Edit/5
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

        // POST: Expositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Date,Location,Context")] Exposition exposition)
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
                return RedirectToAction(nameof(Index));
            }
            return View(exposition);
        }

        // GET: Expositions/Delete/5
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

        // POST: Expositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
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
            return RedirectToAction(nameof(Index));
        }

        private bool ExpositionExists(Guid id)
        {
          return (_context.Exposition?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Models.Domain;
using SistemaGraficosCITIC.Repositories;
using SistemaGraficosCITIC.Views.ViewModels;

namespace SistemaGraficosCITIC.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly SistemaGraficosCITICContext _context;

        private readonly UserManager<IdentityUser> userManager;

        private readonly SignInManager<IdentityUser> signInManager;

        private readonly IResearcherRepository researcherRepository;

        private readonly IProjectRepository projectRepository;

        ProjectModel _projectModel;

        /// <summary>
        /// Constructor of the projectsController class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_researcherRepository"></param>
        /// <param name="_userManager"></param>
        /// <param name="_signInManager"></param>
        /// <param name="_projectRepository"></param>
        public ProjectsController(SistemaGraficosCITICContext context, IResearcherRepository _researcherRepository,
            UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, IProjectRepository _projectRepository)
        {
            _context = context;
            researcherRepository = _researcherRepository;
            userManager = _userManager;
            signInManager = _signInManager;
            projectRepository = _projectRepository;
            _projectModel = new ProjectModel();
        }

        /// <summary>
        /// GET method for the index view of projects
        /// </summary>
        /// <returns>The Task of action to the view</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                var userName = User.Identity!.Name;
                var currentUser = await userManager.FindByNameAsync(userName);
                var id = new Guid(currentUser.Id);
                IndexProjectsViewModel model = new IndexProjectsViewModel();
                model.projects = await projectRepository.GetProjectsByResearcher(id);
                // Publicaciones asociadas
                model.publications = await _context.Publication.Include(x => x.Project)
                    .Include(c => c.Project!.Researcher)
                    .Where(c => c.Project!.Researcher!.Id == id).ToListAsync();
                // Exposiciones asociadas
                model.expositions = await _context.Exposition.Include(e => e.Project)
                    .Include(e => e.Project!.Researcher)
                    .Where(e => e.Project!.Researcher!.Id == id).ToListAsync();
                // Productos asociados
                model.products = await _context.Product.Include(p => p.Project)
                    .Include(p => p.Project!.Researcher)
                    .Where(p => p.Project!.Researcher!.Id == id).ToListAsync();

                return _context.Project != null ?
                              View(model) :
                              Problem("Entity set 'SistemaGraficosCITICContext.Project'  is null.");
            }else{
                return View();
            }
        }

        /// <summary>
        /// GET method for see the details of a project
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Project == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        /// <summary>
        /// GET method for create a project
        /// </summary>
        /// <returns>The Task of action to the view</returns>
        public IActionResult Create()
        {
            return View(_projectModel);
        }

        /// <summary>
        /// POST method for create a project
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User)) {
                    var userName = User.Identity!.Name;
                    var currentUser = await userManager.FindByNameAsync(userName);
                    var id = new Guid(currentUser.Id);
                    var researcher = await researcherRepository.GetAsync(id);
                    if (!model.isActive) {
                        var project = new Project(model.Name!, model.Type!, researcher!, model.StartDate, model.EndDate, model.isActive);
                        _context.Add(project);
                        await _context.SaveChangesAsync();
                        var projectId = project.Id.ToString();
                        return RedirectToAction("Create", "Publications", new { projectId = projectId });
                    }
                    else{
                        model.EndDate = null!;
                        var project = new Project(model.Name!, model.Type!, researcher!,/*Enviar null*/ model.StartDate, model.EndDate, model.isActive);
                        _context.Add(project);
                        await _context.SaveChangesAsync();
                        var projectId = project.Id.ToString();
                        return RedirectToAction("Create", "Publications", new { projectId = projectId });
                    }
                }else{
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction("index", "projects");
        }

        /// <summary>
        /// GET method for edit a project
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Project == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        /// <summary>
        /// POST method for edit a project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="project"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Type,StartDate,EndDate")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            return View(project);
        }

        /// <summary>
        /// GET method for delete a project
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Project == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        /// <summary>
        /// POST method for delete a project
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Project == null)
            {
                return Problem("Entity set 'SistemaGraficosCITICContext.Project'  is null.");
            }
            var project = await _context.Project.FindAsync(id);
            if (project != null)
            {
                _context.Project.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Check of the project exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool tue if the project exists, false if not</returns>
        private bool ProjectExists(Guid id)
        {
          return (_context.Project?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

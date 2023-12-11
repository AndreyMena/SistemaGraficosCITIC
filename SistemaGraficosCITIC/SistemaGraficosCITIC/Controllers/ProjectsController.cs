using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Models.Domain;
using SistemaGraficosCITIC.Repositories;
using SistemaGraficosCITIC.Views.ViewModels;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Specialized;

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
        UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager,
        IProjectRepository _projectRepository)
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
        if (User.IsInRole("Admin"))
        {
          // Si es administrador puede ver todos los proyectos
          model.projects = await projectRepository.GetAllAsyncAdmin();
        }
        else
        {
          // Investigador solo puede ver los proyectos en los que participa
          model.projects = await projectRepository.GetProjectsByResearcher(id);
        }
        // Colaboradores asociados
        model.researchers = await researcherRepository.GetAllAsync();
        // Publicaciones asociadas
        model.publications = await _context.Publication
          .Where(c => c.Project!.ResearcherId == id).ToListAsync();
        // Exposiciones asociadas
        model.expositions = await _context.Exposition
            .Where(e => e.Project!.ResearcherId == id).ToListAsync();
        // Productos asociados
        model.products = await _context.Product
            .Where(p => p.Project!.ResearcherId == id).ToListAsync();
        model.projectResearchers = await _context.ProjectResearcher.ToListAsync();

        return _context.Project != null ?
                      View(model) :
                      Problem("Entity set 'SistemaGraficosCITICContext.Project'  is null.");
      }
      else
      {
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
      var types = _context.ProjectType.ToList(); // Get PublicationTypes from db
      ViewData["projectTypes"] = types; // Pass types list to the view to show it
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
        if (signInManager.IsSignedIn(User))
        {
          var userName = User.Identity!.Name;
          var currentUser = await userManager.FindByNameAsync(userName);
          var id = new Guid(currentUser.Id);
          var researcher = await researcherRepository.GetAsync(id);
          var collab = new List<Researcher>
          {
            researcher // Se introduce a si mismo
          };
          // Introduce de la lista de investigadores colaboradores
          foreach (var coll in model.Collaborators)
          {
            var idR = new Guid(coll);
            var r = await researcherRepository.GetAsync(idR);
            collab.Add(r);
          }
          var project = new Project(model.Name!, model.Type!, model.StartDate, model.EndDate, model.IsActive,
            id/*se agrega el usuario actual como investigador principal*/, model.Code!, collab);
          _context.Add(project);
          await _context.SaveChangesAsync();
          return RedirectToAction("Create", "Projects");
        }
        else
        {
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
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Type,Code,StartDate,EndDate")] Project project)
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
        var publications = await _context.Publication.Where(x => x.Project.Id == id).ToListAsync();
        var products = await _context.Product.Where(x => x.Project.Id == id).ToListAsync();
        var presentations = await _context.Exposition.Where(x => x.Project.Id == id).ToListAsync();
        // Erase possible if there are not any products, publications or Expositions
        if (products.Count == 0 && publications.Count == 0 && presentations.Count == 0)
        {
          _context.Project.Remove(project);
          await _context.SaveChangesAsync();
        }
        else
        {
          await Console.Out.WriteLineAsync("Error borrando proyecto");
        }
      }
      if (User.IsInRole("Admin"))
      {
        return RedirectToAction("Index", "Admin");
      }
      return RedirectToAction(nameof(Index));
    }

    private void Show(object value)
    {
      throw new NotImplementedException();
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

    public async Task<IActionResult> AddProject(ProjectModel model)
    {
      if (ModelState.IsValid)
      {
        if (signInManager.IsSignedIn(User))
        {
          var userName = User.Identity!.Name;
          var currentUser = await userManager.FindByNameAsync(userName);
          var id = new Guid(currentUser.Id);
          var researcher = await researcherRepository.GetAsync(id);
          var collab = new List<Researcher>
          {
            researcher // Se agrega a si mismo a la lista
          };
          foreach (var coll in model.Collaborators)
          {
            var idR = new Guid(coll);
            var r = await researcherRepository.GetAsync(idR);
            collab.Add(r);
          }
          var project = new Project(model.Name!, model.Type!, model.StartDate, model.EndDate, model.IsActive,
            id/*se agrega el usuario actual como investigador principal*/, model.Code!, collab);
          _context.Add(project);
          await _context.SaveChangesAsync();
        }
        else
        {
          return RedirectToAction(nameof(Index));
        }
        return RedirectToAction("Index", "Projects");
      }
      return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ProjectTypes()
    {
      var projectTypes = await _context.ProjectType.ToListAsync();

      if (projectTypes == null)
      {
        return NotFound();
      }
      return View(projectTypes);
    }

    [HttpPost]
    public async Task<IActionResult> ProjectTypes(ProjectTypeModel model, int? check)
    {
      if (ModelState.IsValid)
      {
        if (signInManager.IsSignedIn(User))
        {
          var userName = User.Identity!.Name;
          var currentUser = await userManager.FindByNameAsync(userName);

          var projectType = new ProjectType(
              model.ProjectTypeName
          );
          _context.ProjectType.Add(projectType);
          await _context.SaveChangesAsync();
        }
        else
        {
          return RedirectToAction(nameof(Index));
        }

        return RedirectToAction("ProjectTypes", "Projects");
      }
      return View(model);
    }

    public async Task<IActionResult> DeleteType(int id)
    {
      var projectType = await _context.ProjectType.FindAsync(id);
      if (projectType != null)
      {
        _context.ProjectType.Remove(projectType);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction("ProjectTypes", "Projects");
    }

    public async Task<IActionResult> UpdateResearchers(string[] ids, string projectId)
    {
      if (_context.Project == null)
      {
        return Problem("Entity set 'SistemaGraficosCITICContext.Project'  is null.");
      }
      // List<string> collaboratorIds = Request.Form.TryGetValue("collaborators", col)?.ToList() ?? new List<string>();
      var project = await projectRepository.GetAsync(new Guid(projectId));
      var collab = new List<Researcher>();
      foreach (var id in ids)
      {
        var idR = new Guid(id);
        var r = await researcherRepository.GetAsync(idR);
        collab.Add(r);
      }

      if (project != null)
      {
        DBControllerGetData db = new DBControllerGetData();
        var deleted = db.DeleteResearchersByProject(projectId);
        project.Collaborators = collab;
        _context.Project.Update(project);
        await _context.SaveChangesAsync();
      }
      else
      {
        await Console.Out.WriteLineAsync("Error editando proyecto");
      }
      if (User.IsInRole("Admin"))
      {
        return RedirectToAction("Index", "Admin");
      }
      return RedirectToAction("Index", "Projects");
    }

  }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Models.Domain;
using SistemaGraficosCITIC.Repositories;
using SistemaGraficosCITIC.Views.ViewModels;
using System.Data;

namespace SistemaGraficosCITIC.Controllers
{
    public class PublicationsController : BaseController
    {
        public PublicationsController(SistemaGraficosCITICContext context, SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> _userManager, IProjectRepository _projectRepository) : base(context, _signInManager, _userManager, _projectRepository)
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
                        View(await _context.Publication.ToListAsync()) :
                        Problem("Entity set 'SistemaGraficosCITICContext.Publication'  is null.");
        }

        /// <summary>
        /// GET method for details of a publicationType
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpGet]
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
        /// GET method for create a publicationType
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpGet]
        public async Task<IActionResult> Create(Guid projectId)
        {
            ViewData["projectId"] = projectId;
            var project = await projectRepository.GetAsync(projectId);
            ViewData["projectName"] = project?.Name;
            var types = await _context.PublicationType.ToListAsync(); // Get PublicationTypes from db
            ViewData["type"] = types; // Pass types list to the view to show it
            return View();
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
                    var project = await projectRepository.GetAsync(new Guid(model.ProjectId!));
                    var publication = new Publication(
                        model.PublicationTitle!,
                        model.PublicationYear,
                        model.PublicationReference!,
                        model.PublicationType!,
                        model.PublicationAuthors!,
                        project.Id
                    );
                    
                    _context.Publication.Add(publication);
                    project!.Publications.Add(publication);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["projectId"] = model.ProjectId!;
                model.PublicationYear = 2023;
                model.PublicationTitle = "";
                model.PublicationType = "";
                model.PublicationReference = "";
                model.PublicationAuthors = new string[10];
                ///return View("Create", new PublicationModel());
                return RedirectToAction("Index", "Projects", new { projectId = model.ProjectId });
            }
            return View(model);
        }

        /// <summary>
        /// GET method for edit a publicationType
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Publication == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication.FindAsync(id);
            var authorPublications = await _context.AuthorPublication.Where(x => x.PublicationId == id).ToListAsync();
            //var types = await _context.PublicationTypes.FindAsync();
            List<Author> authors = new List<Author>();
            foreach (var item in authorPublications)
            {
                var author = await _context.Author.FindAsync(item.AuthorId);
                authors.Add(author);
            }
            publication.Authors = authors;
            if (publication == null)
            {
                return NotFound();
            }
            var project = await _context.Project.FindAsync(publication.ProjectId);
            ViewData["projectName"] = project?.Name;
            var types = await _context.PublicationType.ToListAsync(); // Get PublicationTypes from db
            ViewData["type"] = types; // Pass types list to the view to show it

            return View(publication);
        }

        /// <summary>
        /// POST method for edit a publicationType
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publication"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,PublicationYear,Reference,Type,Author,ProjectId")] Publication publication)
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
        /// GET method for delete a publicationType
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpGet]
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
        /// POST method for delete a publicationType
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
                // First delete reference in AuthorPublication table to delete in Publication table
                var publis = _context.AuthorPublication.Where(x => x.PublicationId == id);
                foreach (var item in publis)
                {
                    _context.AuthorPublication.Remove(item);
                }
                _context.Publication.Remove(publication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Projects");
        }

        /// <summary>
        /// Check if the publicationType exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool true if the publicationType exists, false if not/returns>
        private bool PublicationExists(Guid id)
        {
            return (_context.Publication?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Skip method to go to the next page
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>The Task of action to the view</returns>
        public IActionResult Skip(string projectId)
        {
            //var projectId = model.ProjectId;
            return RedirectToAction("Create", "Expositions", new { projectId = projectId });
        }

        /// <summary>
        /// POST method for create a publicationType and go to main page
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> AddToProyect(PublicationModel model)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var userName = User.Identity!.Name;
                    var currentUser = await userManager.FindByNameAsync(userName);
                    var project = await projectRepository.GetAsync(new Guid(model.ProjectId!));

                    var publication = new Publication(
                        model.PublicationTitle!,
                        model.PublicationYear,
                        model.PublicationReference!,
                        model.PublicationType!,
                        model.PublicationAuthors!,
                        project.Id
                    );
                    
                    _context.Publication.Add(publication);
                    project!.Publications.Add(publication);
                    await _context.SaveChangesAsync();
                    //InsertarNuevaPublicacion(publicationType);

                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                var projectId = model.ProjectId;
                return RedirectToAction("Index", "Projects", new { projectId = projectId });
            }
            // Invalid model
            return View(model);
        }

        public bool InsertarNuevaPublicacion(Publication pub)
        {
            bool completado = false;

            string consulta = "InsertarNuevaPublicacion";
            SqlCommand comando = new(consulta);
            comando.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i <= pub.Authors.Count; i++)
            {
                comando.Parameters.AddWithValue("@NewAutor", pub.Authors[i]);
                comando.Parameters.AddWithValue("@IdPubli", pub.Project!.Id);
            }

            SqlParameter completadoExito = new("@InsertCompletado", SqlDbType.Bit);
            completadoExito.Direction = ParameterDirection.Output;
            comando.Parameters.Add(completadoExito);

            if (completadoExito.Value != null)
            {
                completado = Convert.ToBoolean(completadoExito.Value);
            }

            return completado;
        }



        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            var authorList = await _context.Author.ToListAsync();

            authorList = authorList.OrderByDescending(x => x.AuthorName).ToList();

            return authorList;
        }

        [HttpGet]
        public async Task<IActionResult> PublicationTypes()
        {
            var publicationTypes = await _context.PublicationType.ToListAsync();

            if (publicationTypes == null)
            {
                return NotFound();
            }
            return View(publicationTypes);
        }

        [HttpPost]
        public async Task<IActionResult> PublicationTypes(PublicationTypeModel model, int? check)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var userName = User.Identity!.Name;
                    var currentUser = await userManager.FindByNameAsync(userName);

                    var publicationType = new PublicationType(
                        model.PublicationTypeName
                    );
                    _context.PublicationType.Add(publicationType);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction("PublicationTypes", "Publications");
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteType(int id)
        {
            var publicationType = await _context.PublicationType.FindAsync(id);
            if (publicationType != null)
            {
                _context.PublicationType.Remove(publicationType);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("PublicationTypes", "Publications");
        }

    }
}
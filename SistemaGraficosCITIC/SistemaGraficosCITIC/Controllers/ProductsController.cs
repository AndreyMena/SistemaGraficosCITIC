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
    public class ProductsController : Controller
    {
        private readonly SistemaGraficosCITICContext _context;

        private readonly UserManager<IdentityUser> userManager;

        private readonly SignInManager<IdentityUser> signInManager;

        private readonly IProjectRepository projectRepository;

        /// <summary>
        /// Constructor of the ProductsController class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_signInManager"></param>
        /// <param name="_userManager"></param>
        /// <param name="_projectRepository"></param>
        public ProductsController(SistemaGraficosCITICContext context, SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager, IProjectRepository _projectRepository)
        {
            _context = context;
            userManager = _userManager;
            signInManager = _signInManager;
            projectRepository = _projectRepository;
        }

        /// <summary>
        /// GET method for see the index of products
        /// </summary>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Index()
        {
            return _context.Product != null ?
                        View(await _context.Product.ToListAsync()) :
                        Problem("Entity set 'SistemaGraficosCITICContext.Product'  is null.");
        }

        /// <summary>
        /// GET method for see the details of product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        /// <summary>
        /// GET method for create a product
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Create(Guid projectId)
        {
            ViewData["projectId"] = projectId;
            var project = await projectRepository.GetAsync(projectId);
            ViewData["projectName"] = project?.Name;
            return View();
        }

        /// <summary>
        /// POST method for create a product and continue
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContinue(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var userName = User.Identity!.Name;
                    var currentUser = await userManager.FindByNameAsync(userName);

                    string isMarketable = (model.ProductMarketable.Equals("true") ? "Comercializable" : "No comercializable");
                    var product = new Product(model.ProductName, model.ProductDescription, model.ProductState, isMarketable, model.ProductLicense);
                    var project = await projectRepository.GetAsync(new Guid(model.ProjectId));
                    _context.Product.Add(product);
                    project!.Products.Add(product);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                var projectId = model.ProjectId;
                //, new { projectId = projectId });
                return RedirectToAction("Index", "Projects");   // Redirigir a la página inicial de proyectos?
            }
            return View(model);
        }

        /// <summary>
        /// POST method for create a and create again a product
        /// </summary>
        /// <param name="model"></param>
        /// <param name="check"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel model, int? check)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var userName = User.Identity!.Name;
                    var currentUser = await userManager.FindByNameAsync(userName);

                    string isMarketable = (model.ProductMarketable.Equals("true") ? "Comercializable" : "No comercializable");
                    var product = new Product(model.ProductName, model.ProductDescription, model.ProductState, isMarketable, model.ProductLicense);
                    var project = await projectRepository.GetAsync(new Guid(model.ProjectId));
                    _context.Product.Add(product);
                    project!.Products.Add(product);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["projectId"] = model.ProjectId!;
                model.ProductName = "";
                model.ProductDescription = "";
                model.ProductState = "";
                model.ProductMarketable = "";
                model.ProductLicense = "";
                return View("Create", new ProductModel());
            }
            return View(model);
        }

        /// <summary>
        /// GET method for edit a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        /// <summary>
        /// POST method for edit a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,State,Marketable,License")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        /// <summary>
        /// GET method for delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        /// <summary>
        /// POST method for delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Task of action to the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'SistemaGraficosCITICContext.Product' is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Projects");
        }

        /// <summary>
        /// Check of the product exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool tue if the product exists, false if not</returns>
        private bool ProductExists(Guid id)
        {
            return (_context.Product?.Any(p => p.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Skip method to go to the next page
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>The Task of action to the view</returns>)
        public async Task<IActionResult> Skip(string projectId)
        {
            //var projectId = model.ProjectId;
            return RedirectToAction("Index", "Projects", new { projectId = projectId });
        }


    }
}

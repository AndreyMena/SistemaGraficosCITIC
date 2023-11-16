using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Repositories;

namespace SistemaGraficosCITIC.Controllers
{
    public abstract class BaseController : Controller
    {
        public readonly SistemaGraficosCITICContext _context;

        public readonly UserManager<IdentityUser> userManager;

        public readonly SignInManager<IdentityUser> signInManager;

        public readonly IProjectRepository projectRepository;

        /// <summary>
        /// Constructor base
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_signInManager"></param>
        /// <param name="_userManager"></param>
        /// <param name="_projectRepository"></param>
        public BaseController(SistemaGraficosCITICContext context, SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager, IProjectRepository _projectRepository)
        {
            _context = context;
            userManager = _userManager;
            signInManager = _signInManager;
            projectRepository = _projectRepository;
        }
    }
}

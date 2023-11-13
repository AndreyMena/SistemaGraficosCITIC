using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Models.Domain;
using SistemaGraficosCITIC.Repositories;
using SistemaGraficosCITIC.Views.ViewModels;

namespace SistemaGraficosCITIC.Controllers
{
    public abstract class BaseController : Controller
    {
        public readonly SistemaGraficosCITICContext _context;

        public readonly UserManager<IdentityUser> userManager;

        public readonly SignInManager<IdentityUser> signInManager;

        public readonly IProjectRepository projectRepository;

        /// <summary>
        /// Constructor for Publication
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

using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Models.Domain;

namespace SistemaGraficosCITIC.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly SistemaGraficosCITICContext _context;

        public ProjectRepository(SistemaGraficosCITICContext context)
        {
            _context = context;
        }

        public async Task<Project> AddAsync(Project project)
        {
            await _context.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> DeleteAsync(Guid id)
        {
            var existingProject = await _context.Project.FindAsync(id);
            if (existingProject != null)
            {
                _context.Project.Remove(existingProject);
                await _context.SaveChangesAsync();
                return existingProject;
            }
            return null;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            var projectList = await _context.Project.Include(x => x.Researcher).ToListAsync();

            projectList = projectList.OrderByDescending(x => x.StartDate).ToList();

            return projectList;
        }

        public async Task<Project> GetAsync(Guid id)
        {
            return await _context.Project.Include(x => x.Researcher).Include(x => x.Publications).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Project>?> GetProjectsByResearcher(Guid id)
        {
            var projectList = await _context.Project.Include(x => x.Researcher).Where(x => x.Researcher.Id == id).ToListAsync();
            projectList = projectList.OrderByDescending(x => x.StartDate).ToList();
            return projectList;
        }

        public async Task<Project> UpdateAsync(Project project)
        {
            var existingProj = await _context.Project.Include(x => x.Researcher).FirstOrDefaultAsync(x => x.Id == project.Id);

            if (existingProj != null)
            {
                existingProj.Id = project.Id;
                existingProj.Name = project.Name;
                existingProj.Type = project.Type;
                existingProj.StartDate = project.StartDate;
                existingProj.EndDate = project.EndDate;
                existingProj.Collaborators = project.Collaborators;
                existingProj.Code = project.Code;

                await _context.SaveChangesAsync();
                return existingProj;
            }
            return null;
        }
    }
}

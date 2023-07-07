using SistemaGraficosCITIC.Models.Domain;

namespace SistemaGraficosCITIC.Repositories
{
    public interface IProjectRepository
    {
        public Task<IEnumerable<Project>> GetAllAsync();

        public Task<Project?> GetAsync(Guid id);

        public Task<IEnumerable<Project>?> GetProjectsByResearcher(Guid id);

        public Task<Project> AddAsync(Project project);

        public Task<Project?> UpdateAsync(Project project);

        public Task<Project?> DeleteAsync(Guid id);        
    }
}

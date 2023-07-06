using SistemaGraficosCITIC.Models.Domain;

namespace SistemaGraficosCITIC.Repositories
{
    public interface IResearcherRepository
    {
        public Task<IEnumerable<Researcher>> GetAllAsync();

        public Task<Researcher?> GetAsync(Guid id);

        public Task<Researcher> AddAsync(Researcher researcher);  
    }
}

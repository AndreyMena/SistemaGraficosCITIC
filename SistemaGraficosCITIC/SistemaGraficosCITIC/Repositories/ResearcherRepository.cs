using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Models.Domain;

namespace SistemaGraficosCITIC.Repositories
{
    public class ResearcherRepository : IResearcherRepository
    {
        private readonly SistemaGraficosCITICContext _context;

        public ResearcherRepository(SistemaGraficosCITICContext context)
        {
            _context = context;
        }

        public async Task<Researcher> AddAsync(Researcher researcher)
        {
            await _context.AddAsync(researcher);
            await _context.SaveChangesAsync();
            return researcher;
        }

        public async Task<IEnumerable<Researcher>> GetAllAsync()
        {
            var researchersList = await _context.Researcher.ToListAsync();

            researchersList = researchersList.OrderByDescending(x => x.LastName).ToList();

            return researchersList;
        }

        public async Task<Researcher?> GetAsync(Guid id)
        {
            return await _context.Researcher.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

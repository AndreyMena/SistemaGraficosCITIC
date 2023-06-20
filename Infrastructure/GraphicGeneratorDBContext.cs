using Domain.Entities;
using Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GraphicGeneratorDBContext : ApplicationDbContext
    {
        public GraphicGeneratorDBContext(DbContextOptions<GraphicGeneratorDBContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admin { get; set; } = null!;
        public DbSet<Researcher> Researcher { get; set; } = null!;
        public DbSet<Question> Question { get; set; } = null!;
        public DbSet<Answer> Answer { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

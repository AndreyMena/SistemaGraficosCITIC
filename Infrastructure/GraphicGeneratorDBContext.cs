using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GraphicGeneratorDBContext : DbContext
    {
        public GraphicGeneratorDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Researcher> Researcher { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

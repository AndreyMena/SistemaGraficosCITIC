using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SistemaGraficosCITIC.Data;

public class SistemaGraficosCITICContext : IdentityDbContext<IdentityUser>
{
    public SistemaGraficosCITICContext(DbContextOptions<SistemaGraficosCITICContext> options)
        : base(options)
    {
    }
    public DbSet<Admin> Admin { get; set; } = null!;
    public DbSet<Researcher> Researcher { get; set; } = null!;
    public DbSet<Question> Question { get; set; } = null!;
    public DbSet<Answer> Answer { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Models.Domain;
using System.Reflection.Emit;

namespace SistemaGraficosCITIC.Data;

public class SistemaGraficosCITICContext : DbContext
{
    public SistemaGraficosCITICContext(DbContextOptions<SistemaGraficosCITICContext> options)
        : base(options)
    {
    }
    public DbSet<Admin> Admin { get; set; } = null!;
    public DbSet<Researcher> Researcher { get; set; } = null!;
    public DbSet<Project> Project { get; set; } = null!;
    public DbSet<Publication> Publication { get; set; } = null!;
    public DbSet<Exposition> Exposition { get; set; } = null!;
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<Author> Author { get; set; } = null!;
    public DbSet<AuthorPublication> AuthorPublication { get; set; } = null!;
    public DbSet<PublicationType> PublicationType { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.Entity<PublicationAuthor>().HasNoKey();
        builder
            .Entity<Publication>()
            .HasMany(c => c.Authors)
            .WithMany(e => e.Publications)
            .UsingEntity<AuthorPublication>(ce => ce.HasOne(c => c.Author)
                                                    .WithMany().HasForeignKey(c => c.AuthorId),
                                            ce => ce.HasOne(e => e.Publication)
                                                    .WithMany().HasForeignKey(r => r.PublicationId))
            .HasKey(ce => new { ce.AuthorId, ce.PublicationId });
        //.Map(m =>
        //    {
        //        m.MapLeftKey("PublicationId");
        //        m.MapRightKey("AuthorId");
        //        m.ToTable("Authors");
        //    }
        //);

        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        /*
         Use [DatabaseSistemaGraficosCITIC]
        ALTER TABLE [dbo].[Publication]
        ADD CONSTRAINT FK_Publication_Project
        FOREIGN KEY (ProjectId) REFERENCES dbo.Project(Id)
        ON DELETE CASCADE;

        ALTER TABLE [dbo].[Exposition]
        ADD CONSTRAINT FK_Exposition_Project
        FOREIGN KEY (ProjectId) REFERENCES dbo.Project(Id)
        ON DELETE CASCADE;

        ALTER TABLE [dbo].[Product]
        ADD CONSTRAINT FK_Product_Project
        FOREIGN KEY (ProjectId) REFERENCES dbo.Project(Id)
        ON DELETE CASCADE;
         */
    }
}

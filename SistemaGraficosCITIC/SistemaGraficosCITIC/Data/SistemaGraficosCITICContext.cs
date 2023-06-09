﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Models.Domain;

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
    protected override void OnModelCreating(ModelBuilder builder)
    {
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

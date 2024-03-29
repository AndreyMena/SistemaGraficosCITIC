﻿using FluentAssertions;
using SistemaGraficosCITIC.Models.Domain;
using Xunit;

namespace UnitTesting.Domain
{
    public class ProjectTest
    {
        [Fact]
        public void ProjectTestInstanceProject()
        {
            // arrange
            var date = new DateTime();
            var researcher = new Researcher();
            var id = Guid.NewGuid();
            var projectTest = new
            {
                Id = id,
                Name = "Project",
                Type = "Accion social",
                StartDate = date,
                EndDate = date,
                IsActive = true,
                Researcher = researcher, //Por ahora
                Publications = new List<Publication>(),
                Expositions = new List<Exposition>(),
                Products = new List<Product>()
            };
            // act 
            var project = new Project("Project", "Accion social", researcher, date, date, true);
            project.Id = id;
            // assert
            project.Should().BeEquivalentTo(projectTest);
        }

        [Fact]
        public void ProjectTestInstanceProjectEmpty()
        {
            // arrange
            var date = new DateTime();
            var researcher = new Researcher();
            var id = Guid.NewGuid();
            var projectTest = new
            {
                Id = id,
                Name = ""!,
                Type = ""!,
                EndDate = DateTime.MinValue,
                Publications = new List<Publication>(),
                Expositions = new List<Exposition>(),
                Products = new List<Product>()
            };
            // act 
            var project = new Project();
            project.Id = id;
            // assert
            project.Should().BeEquivalentTo(projectTest);
        }
    }
}

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Models.Domain;
using SistemaGraficosCITIC.Repositories;
using Xunit;

namespace UnitTesting.Repositories
{
    public class ProjectRepositoryTest
    {
        [Fact]
        public async void GetAllProjectTest()
        {
            // arrange
            var projectCountTest = 4;

            var options = new DbContextOptionsBuilder<SistemaGraficosCITICContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseProjectRepositoryTestGetAllProjectTest")
                .Options;

            var date = new DateTime();
            var researcher = new Researcher();
            var listResearcher = new List<Researcher>();
            // Insert seed data into the database using one instance of the context
            using (var context = new SistemaGraficosCITICContext(options))
            {
                context.Project.Add(new Project("Project1", "Accion social", date, date, true, researcher.Id, "1", listResearcher));
                context.Project.Add(new Project("Project2", "Accion social", date, date, true, researcher.Id, "2", listResearcher));
                context.Project.Add(new Project("Project3", "Accion social", date, date, true, researcher.Id, "3", listResearcher));
                context.Project.Add(new Project("Project4", "Accion social", date, date, true, researcher.Id, "4", listResearcher));

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new SistemaGraficosCITICContext(options))
            {
                ProjectRepository projectRepository = new(context);

                // act
                var project = await projectRepository.GetAllAsync();

                // assert
                project.Should().HaveCount(projectCountTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void AddProjectTest()
        {
            // arrange
            var projectCountTest = 4;

            var options = new DbContextOptionsBuilder<SistemaGraficosCITICContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseProjectRepositoryTestAddProjectTest")
                .Options;

            var date = new DateTime();
            var researcher = new Researcher();
            var listResearcher = new List<Researcher>();
            // Insert seed data into the database using one instance of the context

            // Use a clean instance of the context to run the test
            using var context = new SistemaGraficosCITICContext(options);
            ProjectRepository projectRepository = new(context);

            // act
            await projectRepository.AddAsync(new Project("Project1", "Accion social", date, date, true, researcher.Id, "1", listResearcher));
            await projectRepository.AddAsync(new Project("Project2", "Accion social", date, date, true, researcher.Id, "2", listResearcher));
            await projectRepository.AddAsync(new Project("Project3", "Accion social", date, date, true, researcher.Id, "3", listResearcher));
            await projectRepository.AddAsync(new Project("Project4", "Accion social", date, date, true, researcher.Id, "4", listResearcher));
            var project = await projectRepository.GetAllAsync();

            // assert
            project.Should().HaveCount(projectCountTest);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async void DeleteProjectTest()
        {
            // arrange
            var projectCountTest = 2;

            var options = new DbContextOptionsBuilder<SistemaGraficosCITICContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseProjectRepositoryTestDeleteProjectTest")
                .Options;

            var date = new DateTime();
            var researcher = new Researcher();
            var listResearcher = new List<Researcher>();
            // Insert seed data into the database using one instance of the context
            var project1 = new Project("Project1", "Accion social", date, date, true, researcher.Id, "1", listResearcher);
            var id1 = project1.Id;
            var project2 = new Project("Project2", "Accion social", date, date, true, researcher.Id, "2", listResearcher);
            var id2 = project2.Id;
            var project3 = new Project("Project3", "Accion social", date, date, true, researcher.Id, "3", listResearcher);
            var id3 = project3.Id;
            var project4 = new Project("Project4", "Accion social", date, date, true, researcher.Id, "4", listResearcher);
            var id4 = project4.Id;
            using (var context = new SistemaGraficosCITICContext(options))
            {
                context.Project.Add(project1);
                context.Project.Add(project2);
                context.Project.Add(project3);
                context.Project.Add(project4);

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new SistemaGraficosCITICContext(options))
            {
                ProjectRepository projectRepository = new(context);

                // act
                await projectRepository.DeleteAsync(id1);
                await projectRepository.DeleteAsync(id2);
                var project = await projectRepository.GetAllAsync();

                // assert
                project.Should().HaveCount(projectCountTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void GetAsyncByIdProjectTest()
        {
            // arrange
            // var projectCountTest = 2;

            var options = new DbContextOptionsBuilder<SistemaGraficosCITICContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseProjectRepositoryTestGetAsyncByIdProjectTest")
                .Options;

            var date = new DateTime();
            var researcher = new Researcher();
            var listResearcher = new List<Researcher>();
            // Insert seed data into the database using one instance of the context
            var project1 = new Project("Project1", "Accion social", date, date, true, researcher.Id, "1", listResearcher);
            var id1 = project1.Id;
            var project2 = new Project("Project2", "Accion social", date, date, true, researcher.Id, "2", listResearcher);
            var project3 = new Project("Project3", "Accion social", date, date, true, researcher.Id, "3", listResearcher);
            var project4 = new Project("Project4", "Accion social", date, date, true, researcher.Id, "4", listResearcher);
            using (var context = new SistemaGraficosCITICContext(options))
            {
                context.Project.Add(project1);
                context.Project.Add(project2);
                context.Project.Add(project3);
                context.Project.Add(project4);

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new SistemaGraficosCITICContext(options))
            {
                ProjectRepository projectRepository = new(context);

                // act
                var project = await projectRepository.GetAsync(id1);

                // assert
                project.Should().BeEquivalentTo(project1);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void GetProjectsByResearcherProjectTest()
        {
            // arrange
            var projectCountTest = 1;

            var options = new DbContextOptionsBuilder<SistemaGraficosCITICContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseProjectRepositoryTestGetProjectsByResearcherTest")
                .Options;

            var date = new DateTime();
            var researcher1 = new Researcher();
            var researcher2 = new Researcher();
            var researcher3 = new Researcher();
            var researcher4 = new Researcher();
            var listResearcher = new List<Researcher>();
            // Insert seed data into the database using one instance of the context
            var project1 = new Project("Project1", "Accion social", date, date, true, researcher1.Id, "1", listResearcher);
            var id1 = project1.Id;
            var project2 = new Project("Project2", "Accion social", date, date, true, researcher2.Id, "2", listResearcher);
            var id2 = project2.Id;
            var project3 = new Project("Project3", "Accion social", date, date, true, researcher3.Id, "3", listResearcher);
            var id3 = project3.Id;
            var project4 = new Project("Project4", "Accion social", date, date, true, researcher4.Id, "4", listResearcher);
            var id4 = project4.Id;
            using (var context = new SistemaGraficosCITICContext(options))
            {
                context.Project.Add(project1);
                context.Project.Add(project2);
                context.Project.Add(project3);
                context.Project.Add(project4);

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new SistemaGraficosCITICContext(options))
            {
                ProjectRepository projectRepository = new(context);

                // act
                var project = await projectRepository.GetProjectsByResearcher(researcher1.Id);

                // assert
                project.Should().HaveCount(projectCountTest);

                context.Database.EnsureDeleted();
            }
        }
    }
}

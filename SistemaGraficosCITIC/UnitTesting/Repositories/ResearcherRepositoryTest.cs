using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Data;
using SistemaGraficosCITIC.Models.Domain;
using SistemaGraficosCITIC.Repositories;

namespace UnitTesting.Repositories
{
    public class ResearcherRepositoryTest
    {
        [Fact]
        public async void GetAllResearcherTest()
        {
            // arrange
            var researcherCountTest = 4;

            var options = new DbContextOptionsBuilder<SistemaGraficosCITICContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseResearcherRepositoryTestGetAllResearcherTest")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new SistemaGraficosCITICContext(options))
            {
                context.Researcher.Add(new Researcher("Name1", "LastName1", "Type"));
                context.Researcher.Add(new Researcher("Name2", "LastName2", "Type"));
                context.Researcher.Add(new Researcher("Name3", "LastName3", "Type"));
                context.Researcher.Add(new Researcher("Name4", "LastName4", "Type"));

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new SistemaGraficosCITICContext(options))
            {
                ResearcherRepository researcherRepository = new ResearcherRepository(context);

                // act
                var researchers = await researcherRepository.GetAllAsync();

                // assert
                researchers.Should().HaveCount(researcherCountTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void AddResearcherTest()
        {
            // arrange
            var researcherCountTest = 4;

            var options = new DbContextOptionsBuilder<SistemaGraficosCITICContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseResearcherRepositoryTestAddResearcherTest")
                .Options;

            using (var context = new SistemaGraficosCITICContext(options))
            {
                ResearcherRepository projectRepository = new ResearcherRepository(context);

                // act
                await projectRepository.AddAsync(new Researcher("Name1", "LastName1", "Type"));
                await projectRepository.AddAsync(new Researcher("Name2", "LastName2", "Type"));
                await projectRepository.AddAsync(new Researcher("Name3", "LastName3", "Type"));
                await projectRepository.AddAsync(new Researcher("Name4", "LastName4", "Type"));
                var project = await projectRepository.GetAllAsync();

                // assert
                project.Should().HaveCount(researcherCountTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void GetByIdResearcherTest()
        {
            var options = new DbContextOptionsBuilder<SistemaGraficosCITICContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseResearcherRepositoryTestGetByIdResearcherTest")
                .Options;


            var researcher1 = new Researcher("Name1", "LastName1", "Type");
            var researcher2 = new Researcher("Name1", "LastName1", "Type");
            var researcher3 = new Researcher("Name1", "LastName1", "Type");
            var researcher4 = new Researcher("Name1", "LastName1", "Type");
            var id2 = researcher2.Id;
            // Insert seed data into the database using one instance of the context
            using (var context = new SistemaGraficosCITICContext(options))
            {
                context.Researcher.Add(researcher1);
                context.Researcher.Add(researcher2);
                context.Researcher.Add(researcher3);
                context.Researcher.Add(researcher4);

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new SistemaGraficosCITICContext(options))
            {
                ResearcherRepository researcherRepository = new ResearcherRepository(context);

                // act
                var researcher = await researcherRepository.GetAsync(id2);

                // assert
                researcher.Should().BeEquivalentTo(researcher2);

                context.Database.EnsureDeleted();
            }
        }
    }
}

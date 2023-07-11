using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FluentAssertions;
using SistemaGraficosCITIC.Models.Domain;
using Xunit;

namespace UnitTesting.Domain
{
    public class ProductTest
    {
        [Fact]
        public void ProjectTestInstanceProject()
        {
            // arrange
            var id = Guid.NewGuid();
            var projectTest = new
            {
                Id = id,
                Name = "Name",
                Description = "Description",
                State = "State",
                Marketable = "Marketable",
                License = "License"
            };
            // act 
            var project = new Product("Name", "Description", "State", "Marketable", "License");
            project.Id = id;
            // assert
            project.Should().BeEquivalentTo(projectTest);
        }

        [Fact]
        public void ProjectTestInstanceProjectEmpty()
        {
            // arrange
            var id = Guid.NewGuid();
            var projectTest = new
            {
                Id = id,
                Name = "",
                Description = "",
                State = "",
                Marketable = "",
                License = ""
            };
            // act 
            var project = new Product();
            project.Id = id;
            // assert
            project.Should().BeEquivalentTo(projectTest);
        }
    }
}

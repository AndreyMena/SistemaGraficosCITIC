using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FluentAssertions;
using SistemaGraficosCITIC.Models.Domain;
using Xunit;

namespace UnitTesting.Domain
{
    public class ResearcherTest
    {
        [Fact]
        public void ResearcherTestInstanceResearcher()
        {
            // arrange
            var id = Guid.NewGuid();
            var researcherTest = new
            {
                Id = id,
                Name = "Name",
                LastName = "LastName",
                Type = "Type"
            };
            // act 
            var researcher = new Researcher("Name", "LastName", "Type");
            researcher.Id = id;
            // assert
            researcher.Should().BeEquivalentTo(researcherTest);
        }

        [Fact]
        public void ResearcherTestInstanceResearcherEmpty()
        {
            // arrange
            var id = Guid.NewGuid();
            var researcherTest = new
            {
                Id = id,
                Name = "",
                LastName = "",
                Type = ""
            };
            // act 
            var researcher = new Researcher();
            researcher.Id = id;
            // assert
            researcher.Should().BeEquivalentTo(researcherTest);
        }
    }
}

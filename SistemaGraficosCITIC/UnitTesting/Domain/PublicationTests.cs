using FluentAssertions;
using SistemaGraficosCITIC.Models.Domain;
using Xunit;

namespace UnitTesting.Domain
{
    public class PublicationTests
    {
        [Fact]
        public void PublicationTestInstancePublication()
        {
            // arrange
            var date = new DateTime();
            var id = Guid.NewGuid();
            var projectTest = new
            {
                Id = id,
                Title = "Publication",
                Date = date,
                Reference = "Referencia",
                Type = "Type"
            };
            // act 
            var Publication = new Publication("Publication", date, "Referencia", "Type");
            Publication.Id = id;
            // assert
            Publication.Should().BeEquivalentTo(projectTest);
        }

        [Fact]
        public void PublicationTestInstancePublicationEmpty()
        {
            // arrange
            var id = Guid.NewGuid();
            var projectTest = new
            {
                Id = id,
                Title = "",
                Date = DateTime.MinValue,
                Reference = "",
                Type = ""
            };
            // act 
            var Publication = new Publication();
            Publication.Id = id;
            // assert
            Publication.Should().BeEquivalentTo(projectTest);
        }
    }
}

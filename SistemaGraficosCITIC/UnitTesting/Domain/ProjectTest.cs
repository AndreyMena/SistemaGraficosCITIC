using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using SistemaGraficosCITIC.Models.Domain;

namespace UnitTesting.Domain
{
    public class ProjectTest
    {
        [Fact]
        public void PeopleTestInstancePerson()
        {
            // arrange
            /*
            var projectTest = new
            {
                Id = "Email1",
                Name = RequiredString.TryCreate("Marcelo").Success(),
                LastName1 = RequiredString.TryCreate("Jenkins").Success(),
                LastName2 = RequiredString.TryCreate("Coronas").Success(),
                HighestDegree = PersonHighestDegree.TryCreate("Dr.").Success(),
                University = RequiredString.TryCreate("Universidad de Costa Rica").Success(),
                ProfilePicture = profilePic,
                CoordinatorGroups = coordinatorGroups,
                ResearchGroup = researchGroup
            };*/

            // act 
            var person = new Project("Project", "Accion social", null, new DateTime(), new DateTime(), true);

            // assert
            //person.Should().BeEquivalentTo(personTest);
        }
    }
}

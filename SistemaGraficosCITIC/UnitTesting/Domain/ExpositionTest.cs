﻿using SistemaGraficosCITIC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.CodeAnalysis;


namespace UnitTesting.Domain
{
    public class ExpositionTest
    {
        [Fact]
        public void ExpositionTestInstanceExposition()
        {
            // arrange
            var date = new DateTime();
            var id = Guid.NewGuid();
            var expositionTest = new
            {
                Id = id,
                Date = date,
                Location = "Location",
                Context = "Context"
            };
            // act 
            var exposition = new Exposition(date, "Location", "Context");
            exposition.Id = id;
            // assert
            exposition.Should().BeEquivalentTo(expositionTest);
        }

        [Fact]
        public void ExpositionTestInstanceExpositionEmpty()
        {
            // arrange
            var id = Guid.NewGuid();
            var expositionTest = new
            {
                Id = id,
                Date = DateTime.MinValue,
                Location = "",
                Context = "",
        };
            // act 
            var exposition = new Exposition();
            exposition.Id = id;
            // assert
            exposition.Should().BeEquivalentTo(expositionTest);
        }
    }
}

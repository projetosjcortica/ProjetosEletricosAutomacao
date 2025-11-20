using Domain.Services.Anilhas;
using Domain.Value_Objects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Anilhas
{
    public class FormatarAnilhaCInversorTeste
    {
        [Fact]
        public void Sucess_Case_1()
        {
            var Result = Anilha.CreateAnilhaCInversor("CCM-1A", 2);
            Result.Value.Should().Be("1A-INV2.C");
        }

        [Fact]
        public void Sucess_Case_2()
        {
            var Result = Anilha.CreateAnilhaCInversor("CCM-1B", 10);
            Result.Value.Should().Be("1B-INV10.C");
        }

        [Fact]
        public void Sucess_Case_3()
        {
            var Result = Anilha.CreateAnilhaCInversor("CCM-1D", 101);
            Result.Value.Should().Be("1D-INV101.C");
        }
    }
}

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
    public class FormatarAnilhaR1SoftStarterTeste
    {
        [Fact]
        public void Sucess_Case_1()
        {
            //var result = FormatarAnilhaR1SoftStarter.Execute("R.1.SS1", 2);
            //var result = Anilha.CreateAnilhaSoftStarter("R.1.SS1", 2);
            //result.Value.Should().Be("R.1.SS2");
        }

        [Fact]
        public void Sucess_Case_2()
        {
            //var result = FormatarAnilhaR1SoftStarter.Execute("R.1.SS1", 16);
            //var result = Anilha.CreateAnilhaSoftStarter("R.1.SS1", 16);
            //result.Value.Should().Be("R.1.SS16");
        }
    }
}

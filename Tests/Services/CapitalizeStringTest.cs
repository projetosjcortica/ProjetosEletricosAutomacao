using Domain.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class CapitalizeStringTest
    {
        [Fact]
        public void Sucess_Case_1()
        {
            var result = CapitalizeString.Execute("transporte vibratório 1");

            result.Should().Be("Transporte Vibratório 1");
        }
        [Fact]
        public void Sucess_Case_2()
        {
            var result = CapitalizeString.Execute("Elevador DE CORRENTE SAIDA DE RESIDUOS");
            result.Should().Be("Elevador de Corrente Saida de Residuos");
        }

        [Fact]
        public void Sucess_Case_3()
        {
            var result = CapitalizeString.Execute("R1A");
            result.Should().Be("R1A");
        }
        [Fact]
        public void Sucess_Case_4()
        {
            var result = CapitalizeString.Execute("R15A");
            result.Should().Be("R15A");
        }

        [Fact]
        public void Sucess_Case_5()
        {
            var result = CapitalizeString.Execute("R1005A");
            result.Should().Be("R1005A");
        }

        [Fact]
        public void Sucess_Case_6()
        {
            var result = CapitalizeString.Execute("R1005F");
            result.Should().Be("R1005F");
        }
        
        [Fact]
        public void Sucess_Case_7()
        {
            var result = CapitalizeString.Execute("Módulo de freio do motor (K-EL-1)");
            result.Should().Be("Módulo de Freio do Motor (K-EL-1)");
        }

        [Fact]
        public void Sucess_Case_8()
        {
            var result = CapitalizeString.Execute("PT-100");
            result.Should().Be("PT-100");
        }

        [Fact]
        public void Sucess_Case_9()
        {
            var result = CapitalizeString.Execute("Sensor PT-100 Superior CA-1");
            result.Should().Be("Sensor PT-100 Superior CA-1");
        }

        [Fact]
        public void Sucess_Case_10()
        {
            var result = CapitalizeString.Execute("Sensor PT-100 Superior CA-2");
            result.Should().Be("Sensor PT-100 Superior CA-2");
        }

    }
}

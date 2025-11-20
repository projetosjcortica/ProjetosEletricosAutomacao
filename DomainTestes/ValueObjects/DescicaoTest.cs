using Domain.Value_Objects;
using Domain.Value_Objects.Descricao;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTestes.ValueObjects
{
    public class DescicaoTest
    {
        [Fact]
        public void Sucess_Case_Create_Descricao_Registro()
        {
            var Result = new Descricao("Acionamento / Status Registro 4 Abre / Fecha");
            Result.Value.Should().Be("Acionamento / Status\r\nRegistro 4\r\nAbre / Fecha");
        }

        [Fact]
        public void Sucess_Case_Create_Descricao_Sensor()
        {
            var Result = new Descricao("Status Sensor Pistão 1 Abre");
            Result.Value.Should().Be("Status\r\nSensor Pistão 1 Abre");
        }

        [Fact]
        public void Sucess_Case_Create_Descricao_Botao()
        {
            var Result = new Descricao("Botão Eletrônica Grão Abre / Fecha");
            Result.Value.Should().Be("Botão\r\nEletrônica Grão Abre / Fecha");
        }

        [Fact]
        public void Sucess_Case_Create_Descricao_PT_100()
        {
            var Result = new Descricao("Sensor PT-100 Secador 12");
            Result.Value.Should().Be("Sensor PT-100\r\nSecador 12");
        }

        [Fact]
        public void Sucess_Case_Create_Descricao_Motor()
        {
            var Result = new Descricao("pré limpeza 1c - transmissão");
            Result.Value.Should().Be("Pré Limpeza 1C - Transmissão");
        }

        [Fact]
        public void Sucess_Case_Create_Descricao_Com_Parenteses()
        {
            var Result = new Descricao("alimentação rosca 1 (alimentação rosca tulha)");
            Result.Value.Should().Be("Alimentação Rosca 1 (Alimentação Rosca Tulha)");
        }

    }
}

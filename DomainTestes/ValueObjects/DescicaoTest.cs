using Domain.Value_Objects.Descricao;
using FluentAssertions;

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
            Result.Value.Should().Be("Status Sensor\r\nPistão 1\r\nAbre");
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

        [Fact]
        public void Sucess_Case_Create_Descricao_Registro_Abre()
        {
            var Result = new Descricao("R12A");
            Result.Value.Should().Be("R12A");
        }

        [Fact]
        public void Sucess_Case_Create_Descricao_Registro_Fecha()
        {
            var Result = new Descricao("R1F");
            Result.Value.Should().Be("R1F");
        }
        [Fact]
        public void Sucess_Case_Create_Descricao_Valvula()
        {
            var Result = new Descricao("Acionamento / Status Válvula 4 Abre / Fecha");
            Result.Value.Should().Be("Acionamento / Status\r\nVálvula 4\r\nAbre / Fecha");
        }

        [Fact]
        public void Sucess_Case_Create_Descricao_Atuador()
        {
            var Result = new Descricao("K-AT-35A");
            Result.Value.Should().Be("K-AT-35A");
        }



    }
}

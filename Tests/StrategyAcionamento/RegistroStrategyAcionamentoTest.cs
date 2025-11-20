using Domain.Value_Objects.DatePages.StrategyReconhecimento;
using FluentAssertions;

namespace Tests.StrategyAcionamento
{
    public class RegistroStrategyAcionamentoTest
    {
        [Fact]
        public void Sucess_Case_1()
        {
            var strategy = new RegistroStrategyAcionamento();

            var result = strategy.Execute("Acionamento Registro 15 Abre / Fecha");

            result.Should().Be("Acionamento\r\nRegistro 15\r\nAbre / Fecha");
        }
    }
}

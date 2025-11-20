using Domain.Value_Objects.Partidas;
using FluentAssertions;

namespace DomainTestes.ValueObjects
{
    public class AcionamentoTest
    {
        [Fact]
        public void DeveCapitalizarADescricaoDoAcionamento()
        {
            var result = new Acionamento("TV-1", "ACT", "transporte vibratório 1", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);

            result.Descricao.Value.Should().Be("Transporte Vibratório 1");
        }

        [Fact]
        public void DeveCapitalizarANomenclaturaDoAcionamento()
        {
            var result = new Acionamento("R1A", "ACT", "transporte vibratório 1", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);

            result.Nomenclatura.Value.Should().Be("R1A");
        }

        [Fact]
        public void DeveRetornarUmCartaoAnalogicoDoAcionamento()
        {
            var result = new Acionamento("RTM-1", "ACT", "Rosca Transportadora Moinho 1", "8-AIO-U2", "1A-CT-3.15", "", "", "", "", "", "", 1);

            result.GetData()["texto_cartao_acionamento_1"].Should().Be("XN-322-8-AIO-U2 \r\n Cartão 3 - X4");
        }
    }
}

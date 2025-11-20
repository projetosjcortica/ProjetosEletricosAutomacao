using Domain.Value_Objects.Partidas;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Value_Object.DataPages
{
    public class AcionamentoTest
    {
        [Fact]
        public void DeveCapitalizarADescricaoDoAcionamento()
        {
            var result = new Acionamento("TV-1", "ACT", "transporte vibratório 1", "16-DO","1A-CT-1.1", "1A-ACT-1", "RL01","30", "", "", "", 1);

            result.Descricao.Should().Be("Transporte Vibratório 1");
        }
        [Fact]
        public void DeveCapitalizarANomenclaturaDoAcionamento()
        {
            var result = new Acionamento("R1A", "ACT", "transporte vibratório 1", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);

            result.Nomenclatura.Should().Be("R1A");
        }
    }
}

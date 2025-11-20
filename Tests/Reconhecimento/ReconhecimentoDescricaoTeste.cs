using Domain.Value_Objects.Partidas;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Reconhecimento
{
    public class ReconhecimentoDescricaoTeste
    {
        [Fact]
        public void Should_Description_Atuador_BreakLines()
        {
            var reconhecimento = new Domain.Value_Objects.Partidas.Reconhecimento("R1A","REC", "Reconhecimento Sensor Registro 15 Abre / Fecha", "20-DI", "1A-CT-2.3", "", "", 1);

            reconhecimento.GetData()["descricao_reconhecimento_1"].Should().Be("Reconhecimento Sensor\r\nRegistro 15\r\nAbre / Fecha");
        }
    }
}

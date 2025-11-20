using Domain.Value_Objects.DatePages.StrategyReconhecimento;
using Domain.Value_Objects.Reconhecimento.ReconhecimentoDescricaoHandler;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.StrategyReconhecimento
{
    public class RegistroStrategyTest
    {
        private IReconhecimentoDescricaoHandler reconhecimentoDescricaoHandler = FactoryReconhecimentoHandler.Create();
        [Fact]
        public void Sucess_Case_1()
        {
            var result = reconhecimentoDescricaoHandler.Handle("Reconhecimento Sensor Registro 15 Abre / Fecha");

            result.Should().Be("Reconhecimento Sensor\r\nRegistro 15\r\nAbre / Fecha");
        }
    }
}

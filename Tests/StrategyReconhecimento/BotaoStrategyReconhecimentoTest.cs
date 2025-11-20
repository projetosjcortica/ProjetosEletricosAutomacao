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
    public class BotaoStrategyReconhecimentoTest
    {
        private IReconhecimentoDescricaoHandler reconhecimentoDescricaoHandler = FactoryReconhecimentoHandler.Create();
        [Fact]
        public void Sucess_Case_1()
        {
            var result = reconhecimentoDescricaoHandler.Handle("BT-IN-GR-1-SOBE");

            result.Should().Be("BT-IN\r\nGR-1\r\nSOBE");
        }

        [Fact]
        public void Sucess_Case_2()
        {
            var result = reconhecimentoDescricaoHandler.Handle("BT-IN-GR-1-SOBE-2");

            result.Should().Be("BT-IN\r\nGR-1\r\nSOBE-2");
        }

        [Fact]
        public void Sucess_Case_3()
        {
            var result = reconhecimentoDescricaoHandler.Handle("BT-FA-DB-INF-MAIS");

            result.Should().Be("BT-FA\r\nDB-INF\r\nMAIS");
        }
        [Fact]
        public void Sucess_Case_4()
        {

            var result = reconhecimentoDescricaoHandler.Handle("BT-DB-1");

            result.Should().Be("BT-DB-1");
        }
    }
}

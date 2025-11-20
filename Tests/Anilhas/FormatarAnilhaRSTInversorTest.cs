using Domain.Services.Anilhas;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Anilhas
{
    public class FormatarAnilhaRSTInversorTest
    {
        [Fact]
        public void Testa_Caso_De_Sucesso_1()
        {
            var result = FormatarAnilhasRSTInversor.Execute("CCM-1A", 1);

            result["anilha_inversor_r"].Should().Be("1A-R\r\nINV1");
            result["anilha_inversor_s"].Should().Be("1A-S\r\nINV1");
            result["anilha_inversor_t"].Should().Be("1A-T\r\nINV1");
        }
        [Fact]
        public void Testa_Caso_De_Sucesso_2()
        {
            var result = FormatarAnilhasRSTInversor.Execute("CCM-1A", 2);

            result["anilha_inversor_r"].Should().Be("1A-R\r\nINV2");
            result["anilha_inversor_s"].Should().Be("1A-S\r\nINV2");
            result["anilha_inversor_t"].Should().Be("1A-T\r\nINV2");
        }
        [Fact]
        public void Testa_Caso_De_Sucesso_3()
        {
            var result = FormatarAnilhasRSTInversor.Execute("CCM-1B", 3);

            result["anilha_inversor_r"].Should().Be("1B-R\r\nINV3");
            result["anilha_inversor_s"].Should().Be("1B-S\r\nINV3");
            result["anilha_inversor_t"].Should().Be("1B-T\r\nINV3");
        }

        [Fact]
        public void Testa_Caso_De_Sucesso_4()
        {
            var result = FormatarAnilhasRSTInversor.Execute("CCM-1C", 16);

            result["anilha_inversor_r"].Should().Be("1C-R\r\nINV16");
            result["anilha_inversor_s"].Should().Be("1C-S\r\nINV16");
            result["anilha_inversor_t"].Should().Be("1C-T\r\nINV16");
        }
    }
}

using Domain.Value_Objects;
using FluentAssertions;

namespace Tests.Value_Object.Common
{
    public class CartaoTest
    {
        [Fact]
        public void Deve_Adicionar_Uma_Quebra_de_Linha_Antes_e_Depois_De_PCNT()
        {
            var cartao = new Cartao("20-DI-PCNT");

            cartao.Value.Should().Be("20-DI\r\nPCNT\r\n");
        }
    }
}

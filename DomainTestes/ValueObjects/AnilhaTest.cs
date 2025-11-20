using Domain.Services.Anilhas;
using Domain.Value_Objects;
using FluentAssertions;

namespace DomainTestes.ValueObjects
{
    public class AnilhaTest
    {
        [Fact]
        public void Sucess_Case_Create_Anilha_CCM_1A_And_Index_2()
        {
            var Result = Anilha.CreateAnilhaCInversor("CCM-1A", 2);
            Result.Value.Should().Be("1A-INV2.C");
        }

        [Fact]
        public void Sucess_Case_Create_Anilha_CCM_1B_And_Index_10()
        {
            var Result = Anilha.CreateAnilhaCInversor("CCM-1B", 10);
            Result.Value.Should().Be("1B-INV10.C");
        }

        [Fact]
        public void Sucess_Case_Create_Anilha_CCM_1D_And_Index_101()
        {
            var Result = Anilha.CreateAnilhaCInversor("CCM-1D", 101);
            Result.Value.Should().Be("1D-INV101.C");
        }

        [Fact]
        public void Sucess_Case_Create_Anilha_Soft_Starter_And_Index_2()
        {
            var result = Anilha.CreateAnilhaSoftStarter("CCM-1A",2);
            result.Value.Should().Be("1A-R.SS.2");
        }

        [Fact]
        public void Sucess_Case_Create_Anilha_Soft_Starter_And_Index_16()
        {
            var result = Anilha.CreateAnilhaSoftStarter("CCM-1C",16);
            result.Value.Should().Be("1C-R.SS.16");
        }

        [Fact]
        public void Sucess_Case_Create_Anilha_Inversor_RST_CCM_1A_Index_1()
        {
            var anilhaR = Anilha.CreateAnilhaRInversor("CCM-1A", 1);
            var anilhaS = Anilha.CreateAnilhaSInversor("CCM-1A", 1);
            var anilhaT = Anilha.CreateAnilhaTInversor("CCM-1A", 1);
            anilhaR.Value.Should().Be("1A-R\r\nINV1");
            anilhaS.Value.Should().Be("1A-S\r\nINV1");
            anilhaT.Value.Should().Be("1A-T\r\nINV1");
        }
        [Fact]
        public void Sucess_Case_Create_Anilha_Inversor_RST_CCM_1A_Index_2()
        {
            var anilhaR = Anilha.CreateAnilhaRInversor("CCM-1A", 2);
            var anilhaS = Anilha.CreateAnilhaSInversor("CCM-1A", 2);
            var anilhaT = Anilha.CreateAnilhaTInversor("CCM-1A", 2);
            anilhaR.Value.Should().Be("1A-R\r\nINV2");
            anilhaS.Value.Should().Be("1A-S\r\nINV2");
            anilhaT.Value.Should().Be("1A-T\r\nINV2");
        }
        [Fact]
        public void Sucess_Case_Create_Anilha_Inversor_RST_CCM_1B_Index_3()
        {
            var anilhaR = Anilha.CreateAnilhaRInversor("CCM-1B", 3);
            var anilhaS = Anilha.CreateAnilhaSInversor("CCM-1B", 3);
            var anilhaT = Anilha.CreateAnilhaTInversor("CCM-1B", 3);
            anilhaR.Value.Should().Be("1B-R\r\nINV3");
            anilhaS.Value.Should().Be("1B-S\r\nINV3");
            anilhaT.Value.Should().Be("1B-T\r\nINV3");
        }

        [Fact]
        public void Sucess_Case_Create_Anilha_Inversor_RST_CCM_1C_Index_16()
        {
            var anilhaR = Anilha.CreateAnilhaRInversor("CCM-1C", 16);
            var anilhaS = Anilha.CreateAnilhaSInversor("CCM-1C", 16);
            var anilhaT = Anilha.CreateAnilhaTInversor("CCM-1C", 16);
            anilhaR.Value.Should().Be("1C-R\r\nINV16");
            anilhaS.Value.Should().Be("1C-S\r\nINV16");
            anilhaT.Value.Should().Be("1C-T\r\nINV16");
        }

        //[Fact]
        //public void Sucess_Case_Create_Anilha_Fonte_Atuador_Positiva()
        //{
        //    var result = Anilha.CreateAnilhaFonteAtuadorPositiva("CCM-1A", 2);
        //    result.Value.Should().Be("1A-FDC.2(+)");
        //}

        //[Fact]
        //public void Sucess_Case_Create_Anilha_Fonte_Atuador_Negativa()
        //{
        //    var result = Anilha.CreateAnilhaFonteAtuadorNegativa("CCM-1B", 4);
        //    result.Value.Should().Be("1B-FDC.4(-)");
        //}

        [Fact]
        public void Sucess_Case_Get_Painel_CCM_1A()
        {
            var Result = new Anilha("1a-CT-13.1");
            Result.GetPainel().Should().Be("CCM-1A");
        }

        [Fact]
        public void Sucess_Case_Create_Anilha_Reversao()
        {
            var Result = Anilha.CreateAnilhaReversao("CAR-TRIP-1");
            Result.Value.Should().Be("L2-CAR-TRIP-1");
        }

        [Fact]
        public void Sucess_Case_Create_Anilha_Reversao_Freio_Nomenclatura()
        {
            var Result = Anilha.CreateAnilhaReversao("FR-CAR-TRIP-1");
            Result.Value.Should().Be("L2-CAR-TRIP-1");
        }


        [Fact]
        public void Sucess_Case_Create_Anilha_8AIO_Positiva()
        {
            var cartao = new Cartao("8-AIO-U2");
            var Result = new Anilha("1A-CT-3.1").SetCartao(cartao);
            Result.GetNumeroSaidaCartao().Should().Be("1+");
        }

        [Fact]
        public void Sucess_Case_Create_Anilha_8AIO_Negativa()
        {
            var cartao = new Cartao("8-AIO-U2");
            var Result = new Anilha("1A-CT-3.2").SetCartao(cartao);
            Result.GetNumeroSaidaCartao().Should().Be("1-");
        }

        [Fact]
        public void Sucess_Case_Create_Anilha_8AIO_Numero_Saida_Cartao()
        {
            var cartao = new Cartao("8-AIO-U2");
            var Result = new Anilha("1A-CT-3.15").SetCartao(cartao);
            Result.GetNumeroSaidaCartao().Should().Be("2+");
        }

        [Fact]
        public void Sucess_Case_Create_Anilha_8AIO_Secao_Cartao()
        {
            var cartao = new Cartao("8-AIO-U2");
            var Result = new Anilha("1A-CT-3.15").SetCartao(cartao);
            Result.GetSecaoCartao().Should().Be("4");
        }

    }
}
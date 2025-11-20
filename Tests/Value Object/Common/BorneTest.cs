using Domain.Value_Objects.Common;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Value_Object.Common
{
    public class BorneTest
    {
        [Fact]
        public void Deve_Manter_Formatacao_Padrao_Do_Borne()
        {
            var borne = new Borne("x98B");

            borne.Value.Should().Be("x98B");
        }

        [Fact]
        public void Deve_Adicionar_X_Na_Frente_Do_Borne()
        {
            var borne = new Borne("98B");

            borne.Value.Should().Be("x98B");
        }

        [Fact]
        public void Deve_Adicionar_Remover_Espaços_Em_Branco_Do_Borne()
        {
            var borne = new Borne("x98 B");

            borne.Value.Should().Be("x98B");
        }

        [Fact]
        public void Deve_Adicionar_X_E_Remover_Espaços_Em_Branco_Do_Borne()
        {
            var borne = new Borne("98 B");

            borne.Value.Should().Be("x98B");
        }
    }
}

using Domain.Value_Objects.Common;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTestes.ValueObjects
{
    public class ContatorTest
    {
        [Fact]
        public void Is_Sucess_Case()
        {
            var contator = new Contator("EL-1");
            contator.Value.Should().Be("K-EL-1");
        }

        [Fact]
        public void Is_Car_Tripper_Contator()
        {
            var contator = new Contator("CAR-TRIP-1");
            contator.Value.Should().Be("K-CAR\r\nTRIP-1");
        }

        [Fact]
        public void Is_Contator_Reversao_A()
        {
            var contator = Contator.CreateContatorReversaoA("FR-EL-1");
            contator.Value.Should().Be("K-EL-1A");
        }
    }
}

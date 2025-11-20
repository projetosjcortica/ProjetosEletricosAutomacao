using Domain.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Builders
{
    public class CorelDrawBuilder
    {
        public static ICorelDraw Build()
        {
            return new Mock<ICorelDraw>().Object;
        }
    }
}

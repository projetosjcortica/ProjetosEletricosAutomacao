using FluentAssertions;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ExcelRepositoryTest
    {
        [Fact]
        public void ValidNomenclatura()
        {
            var Repository = new ExcelRepository(@"C:/Users/maqui/Documents/Painel CCM-1A.xlsx", "CCM-1A");

            var DescriptionsPages = Repository.GetDescriptionPages();
        }
    }
}

using FluentAssertions;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class CorelDrawTest
    {
        [Fact]
        public void Sucess()
        {
            var CorelDraw = new CorelDraw(new Domain.Value_Objects.FilePath(""), new Domain.Value_Objects.FilePath(""));

            List<string> Input = new List<string> { "1", "2", "3", "4", "5" };

            var Result = CorelDraw.GroupPagesNumber(Input);
            
            Result.Should().BeEquivalentTo(new List<List<string>>
            {
                new List<string> { "", "2" },
                new List<string> { "1", "3" },
                new List<string> { "2", "4" },
                new List<string> { "3", "5" },
                new List<string> { "4", "" },
            });
        }
    }
}

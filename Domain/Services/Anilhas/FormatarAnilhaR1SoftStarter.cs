using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Services.Anilhas
{
    public static class FormatarAnilhaR1SoftStarter
    {
        public static string Execute(string anilha, int indexSoftStarter)
        {
            var regexPattern = @"SS(\d+)";

            var result = Regex.Replace(anilha, regexPattern, match =>
            {
                return "SS" + indexSoftStarter;
            });

            return result;
        }
    }
}

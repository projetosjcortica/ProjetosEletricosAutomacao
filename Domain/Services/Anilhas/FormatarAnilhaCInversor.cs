using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Services.Anilhas
{
    public static class FormatarAnilhaCInversor
    {
        public static string Execute(string painel, int index)
        {
            var anilha = $"1A-INV{index}.C";
            var regexPattern = @"CCM-(\d[A-Z])";

            var panel = Regex.Replace(painel, regexPattern, "$1");

            var result = anilha.Replace("1A", panel);

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Services.Anilhas
{
    public static class FormatarAnilhasRSTInversor
    {
        public static Dictionary<string, string> Execute(string painel, int index)
        {
            var result = new Dictionary<string, string>();

            result.Add("anilha_inversor_r", FormatarAnilha($"1A-R-INV{index}", painel));
            result.Add("anilha_inversor_s", FormatarAnilha($"1A-S-INV{index}", painel));
            result.Add("anilha_inversor_t", FormatarAnilha($"1A-T-INV{index}", painel));

            return result;
        }

        private static string FormatarAnilha(string anilha, string painel)
        {
            var regexPattern = @"CCM-(\d[A-Z])";

            var panel = Regex.Replace(painel, regexPattern, "$1");

            var result = anilha.Replace("1A", panel);

            result = AdicionarQuebraDeLinha(result);

            return result;
        }

        private static string AdicionarQuebraDeLinha(string anilha)
        {
            return anilha.Replace("-INV", "\r\nINV");
        }
    }
}

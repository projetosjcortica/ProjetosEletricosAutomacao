using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Value_Objects.DatePages.StrategyReconhecimento
{
    public class RegistroStrategyAcionamento
    {
        public string Execute(string description)
        {
            var result = description;

            string regexPattern = @"\s*(Registro \d+)\s*";
            string replacement = "\r\n$1\r\n";

            result = Regex.Replace(result, regexPattern, replacement);

            return result;
        }
    }
}

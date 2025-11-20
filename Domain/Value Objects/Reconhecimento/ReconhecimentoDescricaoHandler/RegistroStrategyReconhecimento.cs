using Domain.Value_Objects.Reconhecimento.ReconhecimentoDescricaoHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Value_Objects.DatePages.StrategyReconhecimento
{
    public class RegistroStrategyReconhecimento : IReconhecimentoDescricaoHandler
    {
        public IReconhecimentoDescricaoHandler? Next { get; set; }

        public string Handle(string description)
        {
            var isPneumatic = description.Contains("Atuador") || description.Contains("Registro");
            if (!isPneumatic) return Next.Handle(description);

            var result = description;

            string regexPattern = @"\s*(Registro \d+)\s*";
            string replacement = "\r\n$1\r\n";

            result = Regex.Replace(result, regexPattern, replacement);

            return result;
        }

        public void SetNext(IReconhecimentoDescricaoHandler next)
        {
            Next = next;
        }
    }
}

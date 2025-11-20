using Domain.Value_Objects.Reconhecimento.ReconhecimentoDescricaoHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Value_Objects.DatePages.StrategyReconhecimento
{
    public class BotaoStrategyReconhecimento : IReconhecimentoDescricaoHandler
    {
        private int MaxLength = 7;

        public IReconhecimentoDescricaoHandler? Next { get ; set; }

        public void SetNext(IReconhecimentoDescricaoHandler next)
        {
            Next = next; 
        }
        public string Handle(string description)
        {
            var isButton = description.Contains("BT");
            if (!isButton) return Next.Handle(description);

            var IsMaxLength = description.Length > MaxLength;

            if (!IsMaxLength)
            {
                return description;
            }

            var result = BreakWithTwoDash(description);

            return result;
        }

        private string BreakWithTwoDash(string description)
        {
            var dashCount = 0;
            var result = "";
            foreach (char c in description)
            {
                var isDash = c == '-';
                if(isDash)
                {
                    dashCount++;
                    if (dashCount == 2)
                    {
                        result += "\r\n";
                        dashCount = 0;
                        continue;
                    }

                    result += c;
                    continue;
                }

                result += c;
            }

            return result;
        }

        
    }
}

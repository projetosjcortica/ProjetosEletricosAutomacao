using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects.Reconhecimento.ReconhecimentoDescricaoHandler
{
    public class DefaultHandler : IReconhecimentoDescricaoHandler
    {
        public IReconhecimentoDescricaoHandler? Next { get; set; }

        public string Handle(string description)
        {
            var Result = string.Empty;
            var formatedDescription = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(description.ToLower());
            var WordsDescricao = formatedDescription.Split(" ");

            for (int i = 0; i < WordsDescricao.Length; i++)
            {
                var currentWord = WordsDescricao[i];
                if (i == WordsDescricao.Length - 1)
                {
                    Result += $"{currentWord}";
                    continue;
                }

                var nextWord = WordsDescricao[i + 1];



                if (currentWord.Length + nextWord.Length > 18)
                {
                    Result += $"{currentWord}\r\n";
                    continue;
                }

                Result += $"{currentWord} {nextWord}\r\n";
                i++;
            }

            return Result;
        }

        public void SetNext(IReconhecimentoDescricaoHandler next)
        {
            Next = next;
        }
    }
}

using System.Text.RegularExpressions;

namespace Domain.Services
{
    public static class CapitalizeString
    {
        public static string Execute(string value)
        {
            var ignoreWords = new List<string> { "da", "do", "de", "das", "dos" };
            var result = new List<string>();

            var words = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var rawWord in words)
            {
                string word = rawWord;
                string prefix = "";
                string suffix = "";

                // Detecta "(" no início
                if (word.StartsWith("("))
                {
                    prefix = "(";
                    word = word.Substring(1);
                }

                // Detecta ")" no final
                if (word.EndsWith(")"))
                {
                    suffix = ")";
                    word = word.Substring(0, word.Length - 1);
                }

                // Hífen → tratar partes separadamente
                if (word.Contains("-"))
                {
                    var parts = word.Split('-');
                    var capParts = parts.Select(p =>
                    {
                        // Se já está totalmente em maiúsculas, mantém
                        if (p.All(char.IsUpper))
                            return p;

                        // Se é numérico, mantém
                        if (p.All(char.IsDigit))
                            return p;

                        // Se tiver letra e número junto (ex: PT100) e começar com letra
                        if (p.Length > 1 && char.IsLetter(p[0]) && p.Skip(1).All(char.IsDigit))
                            return char.ToUpper(p[0]) + p.Substring(1);

                        // Capitalização normal
                        return char.ToUpper(p[0]) + p.Substring(1).ToLower();
                    });

                    result.Add(prefix + string.Join("-", capParts) + suffix);
                    continue;
                }

                // Alfanumérico do tipo 1A, 22B
                if (Regex.IsMatch(word, @"^\d+[A-Za-z]$"))
                {
                    result.Add(prefix + word.ToUpper() + suffix);
                    continue;
                }

                // Palavras ignoradas fora de parênteses
                bool outsideParenthesis = prefix != "(";
                if (outsideParenthesis && ignoreWords.Contains(word.ToLower()))
                {
                    result.Add(prefix + word.ToLower() + suffix);
                    continue;
                }

                // Capitalização normal
                if (word.Length > 0)
                    word = char.ToUpper(word[0]) + word.Substring(1).ToLower();

                result.Add(prefix + word + suffix);
            }

            return string.Join(" ", result);
        }
    }
}

using System.Text.RegularExpressions;

namespace Domain.Services
{
    public static class CapitalizeString
    {
        public static string Execute(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            var ignoreWords = new HashSet<string> { "da", "do", "de", "das", "dos" };
            var result = new List<string>();

            var words = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var rawWord in words)
            {
                string word = rawWord;
                string prefix = "";
                string suffix = "";

                // Casos especiais (Registro / Válvula)
                bool isRegistro = Regex.IsMatch(word, @"^R([1-9][0-9]?|100)[AF]$", RegexOptions.IgnoreCase);
                bool isValvula = Regex.IsMatch(word, @"^V([1-9][0-9]?|100)[AB]$", RegexOptions.IgnoreCase);

                if (isRegistro || isValvula)
                {
                    result.Add(word.ToUpper());
                    continue;
                }

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

                // Tratamento com hífen (K-AT-35A, PT-100, etc.)
                if (word.Contains("-"))
                {
                    var parts = word.Split('-');

                    var capParts = parts.Select(p =>
                    {
                        // Alfanumérico (tem letra e número) → MAIÚSCULO
                        if (Regex.IsMatch(p, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+$"))
                            return p.ToUpper();

                        // Somente números
                        if (p.All(char.IsDigit))
                            return p;

                        // Já está em maiúsculas
                        if (p.All(c => !char.IsLetter(c) || char.IsUpper(c)))
                            return p;

                        // Capitalização normal
                        return char.ToUpper(p[0]) + p.Substring(1).ToLower();
                    });

                    result.Add(prefix + string.Join("-", capParts) + suffix);
                    continue;
                }

                // Alfanumérico simples (1A, 22B)
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

                // Palavra normal
                word = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                result.Add(prefix + word + suffix);
            }

            return string.Join(" ", result);
        }
    }
}

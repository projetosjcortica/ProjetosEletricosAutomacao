using System.Text.RegularExpressions;

namespace Domain.Value_Objects.Descricao.Handles
{
    public class PistaoDescricaoHandler : IDescricaoHandler
    {
        public IDescricaoHandler? Next { get; set; }

        public string Handle(string description)
        {
            var isPneumatic = description.Contains("Pistão");
            if (!isPneumatic) return Next.Handle(description);

            var result = description;

            string regexPattern = @"\s*(Pistão \d+)\s*";
            string replacement = "\r\n$1\r\n";

            result = Regex.Replace(result, regexPattern, replacement);

            return result;
        }

        public void SetNext(IDescricaoHandler next)
        {
            Next = next;
        }
    }
}

namespace Domain.Value_Objects.Descricao.Handles
{
    public class BTDescricaoHandler : IDescricaoHandler
    {
        private const int MaxLength = 7;

        public IDescricaoHandler? Next { get ; set; }

        public void SetNext(IDescricaoHandler next)
        {
            Next = next; 
        }
        public string Handle(string description)
        {
            var isButton = description.Contains("BT");
            if (!isButton) return Next.Handle(description);
            var IsMaxLength = description.Length > MaxLength;
            if (!IsMaxLength || description.Contains("BA"))
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

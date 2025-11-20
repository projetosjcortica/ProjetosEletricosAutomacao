namespace Domain.Value_Objects.Common
{
    public class Borne
    {
        public string Value { get; private set; }
        public Borne(string value)
        {
            Value = NormalizeValue(value);
        }

        private string NormalizeValue(string value)
        {
            var result = value.Replace(" ", "");

            if (!result.StartsWith("x", StringComparison.OrdinalIgnoreCase))
            {
                result = "x" + result;
            }

            return result;
        }
    }
}

namespace Domain.Value_Objects.Common
{
    public class Disjuntor
    {
        public string Value { get; set; }
        public Disjuntor(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Value = string.Empty;
                return;
            }
            var result = value.ToUpper();

            if (IsCarTripper(result))
                Value = $"DM-{result.Replace("CAR-", "CAR\r\n")}";
            else
                Value = $"DM-{result}";
        }

        private bool IsCarTripper(string value)
        {
            return value.Contains("CAR-");
        }
    }
}

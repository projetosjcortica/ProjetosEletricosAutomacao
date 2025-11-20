namespace Domain.Value_Objects.Common
{
    public class Contator
    {
        public string Value { get; }
        public Contator(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Value = string.Empty;
                return;
            }
            var result = value.ToUpper();

            if (IsCarTripper(result))
                Value = $"K-{result.Replace("CAR-", "CAR\r\n")}";
            else
                Value = $"K-{result}";
        }

        private bool IsCarTripper(string value)
        {
            return value.Contains("CAR-");
        }

        public static Contator CreateContatorReversaoA(string nomenclatura)
        {
            return new Contator(nomenclatura.Replace("FR-", "") + "A");
        }

        public static Contator CreateContatorReversaoB(string nomenclatura)
        {
            return new Contator(nomenclatura.Replace("FR-", "") + "B");
        }
    }
}

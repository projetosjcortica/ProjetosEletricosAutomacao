using System.Text.RegularExpressions;

namespace Domain.Value_Objects.Descricao.Handles
{
    public class SensorPT100DescricaoHandler : IDescricaoHandler
    {
        public IDescricaoHandler? Next { get; set; }

        public string Handle(string description)
        {
            var isSensor = description.Contains("Sensor");
            if (!isSensor) return Next.Handle(description);

            var result = description;

            if (description.Contains("PT-100"))
            {
               result = result.Replace("Sensor PT-100 ", "Sensor PT-100\r\n");
            } 
            else
            {
                result = result.Replace("Sensor ", "Sensor\r\n");
            }

            return result;
        }

        public void SetNext(IDescricaoHandler next)
        {
            Next = next;
        }
    }
}

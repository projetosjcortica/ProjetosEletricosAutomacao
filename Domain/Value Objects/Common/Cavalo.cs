using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects.Common
{
    public class Cavalo
    {
        public string Value { get; private set; }
        public Cavalo(string value)
        {
            Value = NormalizeCavalo(value);
        }

        private string NormalizeCavalo(string value)
        {
            var result = value.Replace(" ", "");

            if (!result.EndsWith("CV", StringComparison.OrdinalIgnoreCase))
            {
                result = result + " CV";
            }

            return result;
        }
    }
}

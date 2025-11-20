using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects.Common
{
    public class Nomenclatura
    {
        public string Value { get; }

        public Nomenclatura(string value)
        {
            Value = value.ToUpper();
        }

        public bool IsSoftStarter()
        {
            return Value.Contains("SS-");
        }

        public bool IsInversor()
        {
            return Value.Contains("IF-") || Value.Contains("INV-");
        }
    }
}

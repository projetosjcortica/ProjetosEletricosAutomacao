using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public class Fusivel
    {
        public List<string> Reles { get; set; } = [];
        public Fusivel() { }
        public Fusivel(List<string> reles)
        {
            Reles = reles;
        }

        public void AddRele(string rele)
        {
            Reles.Add(rele);
        }

        public void AddReles(List<string> rele)
        {
            Reles.AddRange(rele);
        }
    }
}

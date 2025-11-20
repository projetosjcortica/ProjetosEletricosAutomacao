using Domain.Value_Objects.Descricao.Handles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects.Common.Descricao.Handlers
{
    public class FreioMotorDescricaoHandler : IDescricaoHandler
    {
        public IDescricaoHandler? Next { get; set; }

        public string Handle(string description)
        {
            var isFreioMotor = description.StartsWith("Freio do Motor");
            if (!isFreioMotor) return Next.Handle(description);

            return description.Replace("Freio do Motor ", "Freio do Motor\r\n");
        }

        public void SetNext(IDescricaoHandler next)
        {
            Next = next;
        }
    }
}

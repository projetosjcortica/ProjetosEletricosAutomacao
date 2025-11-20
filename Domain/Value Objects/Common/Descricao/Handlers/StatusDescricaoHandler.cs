using Domain.Value_Objects.Descricao.Handles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects.Common.Descricao.Handlers
{
    public class StatusDescricaoHandler : IDescricaoHandler
    {
        public IDescricaoHandler? Next { get; set; }

        public string Handle(string description)
        {
            var isStatus = description.StartsWith("Status") || description.StartsWith("Tripper");
            if (!isStatus) return Next.Handle(description);

            if (description.Contains("Tripper"))
                return description.Replace(" - ", "\r\n");

            return description.Replace("Status ", "Status\r\n").Replace("Freio do Motor ", "Freio do Motor\r\n");
        }

        public void SetNext(IDescricaoHandler next)
        {
            Next = next;
        }
    }
}

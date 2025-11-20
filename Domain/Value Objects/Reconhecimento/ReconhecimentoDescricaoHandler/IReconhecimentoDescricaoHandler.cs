using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects.Reconhecimento.ReconhecimentoDescricaoHandler
{
    public interface IReconhecimentoDescricaoHandler
    {
        IReconhecimentoDescricaoHandler? Next { get; set; }
        string Handle(string description);
        void SetNext(IReconhecimentoDescricaoHandler next);
    }
}

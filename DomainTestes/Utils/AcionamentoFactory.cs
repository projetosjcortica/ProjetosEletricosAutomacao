using Domain.Value_Objects.Partidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTestes.Utils
{
    public class AcionamentoFactory
    {
        public Acionamento CreateAcionamento(string nomenclatura, string descricao)
        {
            return new Acionamento(nomenclatura, "ACT", descricao, "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
        }
    }
}

using Domain.Value_Objects.DatePages.StrategyReconhecimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects.Reconhecimento.ReconhecimentoDescricaoHandler
{
    public static class FactoryReconhecimentoHandler
    {
        public static IReconhecimentoDescricaoHandler Create()
        {
            var defaultHandler = new DefaultHandler();
            var RegistroHandler = new RegistroStrategyReconhecimento();
            RegistroHandler.SetNext(defaultHandler);
            var BotaoHandler = new BotaoStrategyReconhecimento();
            BotaoHandler.SetNext(RegistroHandler);

           return BotaoHandler;
        }
    }
}

using Domain.Agreggates;
using Domain.Infrastructure;

namespace Domain.Services.PreDesenho
{
    public class TrocarRSTProUVWService : PreDesenhoServiceBase
    {
        public override void Execute(Page pagina, ICorelDraw corelDraw)
        {
            if (!pagina.GetNomenclatura().Contains("RD-") || pagina.GetNomenclatura().Contains("GRD")) return;
        }
    }
}

using Domain.Agreggates;
using Domain.Infrastructure;
using Domain.Services.ServicosPosProcessamento;

namespace Domain.Services.PreDesenho
{
    public abstract class PreDesenhoServiceBase
    {
        private PreDesenhoServiceBase? _next;

        public PreDesenhoServiceBase SetNext(PreDesenhoServiceBase next)
        {
            _next = next;
            return next;
        }
        public void Handle(Page pagina, ICorelDraw corelDraw)
        {
            Execute(pagina, corelDraw);
            _next?.Handle(pagina, corelDraw);
        }

        public abstract void Execute(Page pagina, ICorelDraw corelDraw);
    }
}

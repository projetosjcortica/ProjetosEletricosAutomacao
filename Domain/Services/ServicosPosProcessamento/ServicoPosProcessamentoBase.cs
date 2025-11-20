using Domain.Agreggates;
using Domain.Infrastructure;

namespace Domain.Services.ServicosPosProcessamento
{
    public abstract class ServicoPosProcessamentoBase
    {
        private ServicoPosProcessamentoBase? _next;

        public ServicoPosProcessamentoBase SetNext(ServicoPosProcessamentoBase next)
        {
            _next = next;
            return next;
        }
        public void Handle(ProjetoComposto project, ICorelDraw corelDraw)
        {
            Execute(project, corelDraw);
            _next?.Handle(project, corelDraw);
        }

        public abstract void Execute(ProjetoComposto project, ICorelDraw corelDraw);
    }
}

using Domain.Services.ProjectServices;
using Domain.Services.ServicosPosProcessamento;

namespace Domain.Factories
{
    public static class PosProcessamentoServicesFactory
    {
        public static ServicoPosProcessamentoBase Create()
        {
            var removerBornesComando = new RemoverBornesComandoService();
            var removerFileiraDeBorneComando = new RemoverFileiraDeBorneComando();
            var removerVentiladoresComando = new RemoverVentiladorComandoService();
            removerBornesComando.SetNext(removerFileiraDeBorneComando);
            removerFileiraDeBorneComando.SetNext(removerVentiladoresComando);


            return removerBornesComando;
        }
    }
}

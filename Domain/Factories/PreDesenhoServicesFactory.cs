using Domain.Services.PreDesenho;

namespace Domain.Factories
{
    public static class PreDesenhoServicesFactory
    {
        public static PreDesenhoServiceBase Create()
        {
            var removerAcionamentoReserva = new RemoverAcionamentoReservaService();
            var removerFusivelReserva = new RemoverFusivelReservaService();
            var removerSensorReserva = new RemoverSensorService();
            var trocarRstProUvw = new TrocarRSTProUVWService();
            removerAcionamentoReserva.SetNext(removerFusivelReserva);
            removerFusivelReserva.SetNext(removerSensorReserva);
            removerSensorReserva.SetNext(trocarRstProUvw);

            return removerAcionamentoReserva;
        }
    }
}

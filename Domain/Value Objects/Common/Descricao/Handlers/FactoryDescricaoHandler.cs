using Domain.Value_Objects.Common.Descricao.Handlers;

namespace Domain.Value_Objects.Descricao.Handles
{
    public static class FactoryDescricaoHandler
    {
        public static IDescricaoHandler Create()
        {
            var defaultHandler = new DefaultHandler();
            var RegistroHandler = new RegistroDescricaoHandler();
            var BTHandler = new BTDescricaoHandler();
            var SensorHandler = new SensorDescricaoHandler();
            var BotaoHandler = new BotaoDescricaoHandler();
            var StatusHandler = new StatusDescricaoHandler();
            var FreioMotorHandler = new FreioMotorDescricaoHandler();
            RegistroHandler.SetNext(defaultHandler);
            BTHandler.SetNext(RegistroHandler);
            SensorHandler.SetNext(BTHandler);
            BotaoHandler.SetNext(SensorHandler);
            StatusHandler.SetNext(BotaoHandler);
            FreioMotorHandler.SetNext(StatusHandler);

           return FreioMotorHandler;
        }
    }
}

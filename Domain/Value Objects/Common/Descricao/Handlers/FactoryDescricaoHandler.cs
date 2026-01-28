using Domain.Value_Objects.Common.Descricao.Handlers;
using System.Text;

namespace Domain.Value_Objects.Descricao.Handles
{
    public static class FactoryDescricaoHandler
    {
        public static IDescricaoHandler Create()
        {
            var defaultHandler = new DefaultHandler();
            var RegistroHandler = new RegistroDescricaoHandler();
            var BTHandler = new BTDescricaoHandler();
            var SensorPT100Handler = new SensorPT100DescricaoHandler();
            var BotaoHandler = new BotaoDescricaoHandler();
            var StatusHandler = new StatusDescricaoHandler();
            var FreioMotorHandler = new FreioMotorDescricaoHandler();
            var ValvulaHandler = new ValvulaDescricaoHandler();
            var pistaoHandler = new PistaoDescricaoHandler();

            RegistroHandler.SetNext(BTHandler);
            BTHandler.SetNext(SensorPT100Handler);
            SensorPT100Handler.SetNext(BotaoHandler);
            BotaoHandler.SetNext(StatusHandler);
            StatusHandler.SetNext(FreioMotorHandler);
            FreioMotorHandler.SetNext(ValvulaHandler);
            ValvulaHandler.SetNext(pistaoHandler);
            pistaoHandler.SetNext(defaultHandler);


           return RegistroHandler;
        }
    }
}

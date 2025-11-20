using Domain.Services.ProjectServices;
using Domain.Services.ServicosPreProcessamento;

namespace Domain.Factories
{
    public static class ProjectServicesFactory
    {
        public static ProjectServiceBase Create()
        {
            var anilhaElevadorService = new AnilhaElevadorService();
            var anilhaSoftStarterService = new AnilhaSoftStarterService();
            var atuadorService = new AtuadorService();
            var freioElevadorService = new FreioElevadorService();
            var reversaoService = new ReversaoService();
            var vemVaiELService = new VemVaiELService();
            var secadorService = new SecadorService();
            var inversorService = new AnilhaInversorService();
            var roscasDosadorasService = new RoscasDosadoraService();
            var moinhoService = new MoinhoService();

            anilhaElevadorService.SetNext(anilhaSoftStarterService);
            anilhaSoftStarterService.SetNext(atuadorService);
            atuadorService.SetNext(freioElevadorService);
            freioElevadorService.SetNext(reversaoService);
            reversaoService.SetNext(vemVaiELService);
            vemVaiELService.SetNext(secadorService);
            secadorService.SetNext(inversorService);
            inversorService.SetNext(roscasDosadorasService);
            roscasDosadorasService.SetNext(moinhoService);

            return anilhaElevadorService;
        }
    }
}

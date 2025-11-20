using Domain.Agreggates;
using Domain.Value_Objects;

namespace Domain.Services.ProjectServices
{
    public class SecadorService : ProjectServiceBase
    {
        public override void Execute(Project project)
        {
            var secadorPaginas = project.Paginas.Where(p => p.GetNomenclatura().Contains("VAL-SEC"));

            var indexSec = 1;
            foreach(var pagina in secadorPaginas)
            {
                var djShape = new Shape("dj_sec", $"DJ-SEC-{indexSec}");
                var valShape = new Shape("valvula_1", $"V{indexSec}");
                var textCaixaRemotoSecador = new Shape("caixa_remoto_secador", $"CAIXA REMOTO SECADOR {indexSec}");

                pagina.AddShape(djShape);
                pagina.AddShape(valShape);
                pagina.AddShape(textCaixaRemotoSecador);

                indexSec++;
            }
        }
    }
}

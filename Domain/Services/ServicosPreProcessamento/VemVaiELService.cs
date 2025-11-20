using Domain.Agreggates;
using Domain.Value_Objects;

namespace Domain.Services.ProjectServices
{
    public class VemVaiELService : ProjectServiceBase
    {
        public override void Execute(Project project)
        {
            var pagesElevadoresEFreio = project.Paginas.Where(pagina => pagina.IsFreioElevadorPage() || pagina.IsElevadorPage()).ToList();

            for (int i = 0; i < pagesElevadoresEFreio.Count - 1; i++)
            {
                var pageElevador = pagesElevadoresEFreio[i];
                var pagesFreioElevador = pagesElevadoresEFreio[i + 1];

                if (pageElevador.IsElevadorPage() && pagesFreioElevador.IsFreioElevadorPage())
                {
                    {
                        var shapeVaiEl = new Shape("vai_el", $"(Vai p/ fl. {pageElevador.PageNumber + 1})");
                        var shapeVemEl = new Shape("vem_el", $"(Vem p/ fl. {pageElevador.PageNumber})");

                        pageElevador.AddShape(shapeVaiEl);
                        pagesFreioElevador.AddShape(shapeVemEl);
                        i++;
                    }
                }
            }
        }
    }
}

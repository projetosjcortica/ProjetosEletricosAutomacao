using Domain.Agreggates;
using Domain.Value_Objects;
using System.Text.RegularExpressions;


namespace Domain.Services.ProjectServices
{
    public class AnilhaElevadorService : ProjectServiceBase
    {
        public override void Execute(Project project)
        {
            var elevadoresPaginas = project.Paginas.Where(pagina => pagina.IsElevadorPage() || pagina.IsFreioElevadorPage()).ToList();
            foreach (var pagina in elevadoresPaginas)
            {
                var match = Regex.Match(pagina.DescriptionPage.Nomenclatura.Value, @"EL-\d+");
                if (match.Success)
                {
                    var shape = new Shape("anilha_elevador", $"L2-{match.Value.Replace("-","")}");
                    pagina.AddShape(shape);
                }
            }
        }
    }
}

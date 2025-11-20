using Domain.Agreggates;
using Domain.Value_Objects;
using System.Text.RegularExpressions;

namespace Domain.Services.ProjectServices
{
    public class FreioElevadorService : ProjectServiceBase
    {
        public override void Execute(Project project)
        {
            var elevadoresPaginas = project.Paginas.Where(pagina => pagina.IsFreioElevadorPage()).ToList();
            foreach(var pagina in elevadoresPaginas)
            {
                var match = Regex.Match(pagina.DescriptionPage.Nomenclatura.Value, @"EL-\d+");
                if (match.Success)
                {
                    var shape = new Shape("k_fr_el", $"K-{match.Value}");
                    var shapeDescricao = new Shape("descricao_freio", $"Freio do Motor\r\nElevador {match.Value.Last()}");
                    pagina.AddShape(shape);
                    pagina.AddShape(shapeDescricao);
                }               
            }
        } 
    }
}

using Domain.Agreggates;
using Domain.Value_Objects;
using System.Text.RegularExpressions;


namespace Domain.Services.ProjectServices
{
    public class MoinhoService : ProjectServiceBase
    {
        public override void Execute(Project project)
        {
            var moinhosPaginas = project.Paginas.Where(pagina => pagina.GetNomenclatura().Contains("MO")).ToList();

            var TcIndex = 1;
            foreach (var pagina in moinhosPaginas)
            {
                pagina.AddShape(new Shape("tc_1", $"TC-{TcIndex}"));
                TcIndex++;
            }
        }
    }
}

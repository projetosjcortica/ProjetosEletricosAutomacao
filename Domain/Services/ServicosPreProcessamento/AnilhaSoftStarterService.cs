using Domain.Agreggates;
using Domain.Value_Objects;

namespace Domain.Services.ProjectServices
{
    public class AnilhaSoftStarterService : ProjectServiceBase
    {
        public override void Execute(Project project)
        {
            int index = 1;
            var softStarterPages = project.Paginas
                                    .Where(pagina => pagina.IsSoftStarterPage())
                                    .ToList();
            foreach (var page in softStarterPages)
            {
                var anilha = Anilha.CreateAnilhaSoftStarter(page.Panel, index);
                var shape = new Shape("r_1_softstarter", anilha.Value);
                page.AddShape(shape);
                index++;
            }

        }
    }
}

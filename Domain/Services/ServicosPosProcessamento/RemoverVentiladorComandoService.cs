using Domain.Agreggates;
using Domain.Infrastructure;
using Domain.Value_Objects;

namespace Domain.Services.ServicosPosProcessamento
{
    internal class RemoverVentiladorComandoService : ServicoPosProcessamentoBase
    {
        public override void Execute(ProjetoComposto project, ICorelDraw corelDraw)
        {
            var paginaComando = corelDraw.FindFirstPageWithShape("ventiladores_1a");

            if (paginaComando == 0) return;

            var totalDePaineis = project.Projetos.Count;
            var deleteShapes = new List<Shape>
                {
                    new Shape("ventiladores_1a", ""),
                    new Shape("ventiladores_1b", ""),
                    new Shape("ventiladores_1c", ""),
                    new Shape("ventiladores_1d", ""),
                    new Shape("ventiladores_1e", ""),
                    new Shape("ventiladores_1f", "")
                };

            var shapesParaDeletar = deleteShapes
                .Skip(project.Projetos.Count)
                .ToList();

            foreach (var shape in shapesParaDeletar)
            {
                corelDraw.DeleteShapeOnPage(paginaComando, shape.Name);
            }
        }
    }
}

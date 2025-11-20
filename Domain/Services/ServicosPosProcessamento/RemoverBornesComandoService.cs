using Domain.Agreggates;
using Domain.Infrastructure;
using Domain.Value_Objects;

namespace Domain.Services.ServicosPosProcessamento
{
    public class RemoverBornesComandoService : ServicoPosProcessamentoBase
    {
        public override void Execute(ProjetoComposto project, ICorelDraw corelDraw)
        {
            var paginaComando = corelDraw.FindFirstPageWithShape("borne_1a");

            if (paginaComando == 0) return;
           
            var totalDePaineis = project.Projetos.Count;
            var deleteShapes = new List<Shape>
                {
                    new Shape("borne_1a", ""),
                    new Shape("borne_1b", ""),
                    new Shape("borne_1c", ""),
                    new Shape("borne_1d", ""),
                    new Shape("borne_1e", ""),
                    new Shape("borne_1f", "")
                };

            int quantidadeParaApagar = 6 - totalDePaineis;

            if (quantidadeParaApagar > 0)
            {
                var shapesParaDeletar = deleteShapes
                    .Skip(deleteShapes.Count - quantidadeParaApagar)
                    .ToList();

                foreach (var shape in shapesParaDeletar)
                {
                    corelDraw.DeleteShapeOnPage(paginaComando, shape.Name);
                }
            }
        }
    }
}

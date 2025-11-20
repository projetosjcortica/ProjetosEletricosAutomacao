using Domain.Agreggates;
using Domain.Infrastructure;
using Domain.Value_Objects;

namespace Domain.Services.PreDesenho
{
    public class RemoverAcionamentoReservaService : PreDesenhoServiceBase
    {
        public override void Execute(Page pagina, ICorelDraw corelDraw)
        {
            if (!pagina.GetNomenclatura().Contains("ACT-RES")) return;

            var deleteAcionamentosReserva = new List<Shape>
            {
                new Shape("acionamento_reserva_1", ""),
                new Shape("acionamento_reserva_2", ""),
                new Shape("acionamento_reserva_3", ""),
                new Shape("acionamento_reserva_4", ""),
                new Shape("acionamento_reserva_5", ""),
                new Shape("acionamento_reserva_6", ""),
            };

            var quantidadeDeAcionamentos = pagina.GetNumeroDeAcionamentos();

            if (quantidadeDeAcionamentos >= deleteAcionamentosReserva.Count) return;


            var shapesParaDeletar = deleteAcionamentosReserva
                .Skip(quantidadeDeAcionamentos)
                .ToList();

            foreach (var shape in shapesParaDeletar)
            {
                corelDraw.DeleteShapeOnPage(pagina.PageNumber, shape.Name);
            }
        }
    }
}

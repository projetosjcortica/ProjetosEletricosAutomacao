using Domain.Agreggates;
using Domain.Infrastructure;
using Domain.Value_Objects;

namespace Domain.Services.PreDesenho
{
    public class RemoverSensorService : PreDesenhoServiceBase
    {
        public override void Execute(Page pagina, ICorelDraw corelDraw)
        {
            if (!pagina.GetNomenclatura().Contains("SENS")) return;

            var deleteSesnores = new List<Shape>
            {
                new Shape("sensor_reserva_1", ""),
                new Shape("sensor_reserva_2", ""),
                new Shape("sensor_reserva_3", ""),
                new Shape("sensor_reserva_4", ""),
                new Shape("sensor_reserva_5", ""),
                new Shape("sensor_reserva_6", ""),
                new Shape("sensor_reserva_7", ""),
                new Shape("sensor_reserva_8", ""),
                new Shape("sensor_reserva_9", ""),
                new Shape("sensor_reserva_10", ""),
                new Shape("sensor_reserva_11", ""),
                new Shape("sensor_reserva_12", ""),
            };

            var quantidadeDeReconhecimentos = pagina.GetNumeroDeReconhecimentos();

            if (quantidadeDeReconhecimentos >= deleteSesnores.Count) return;


            var shapesParaDeletar = deleteSesnores
                .Skip(quantidadeDeReconhecimentos)
                .ToList();

            foreach (var shape in shapesParaDeletar)
            {
                corelDraw.DeleteShapeOnPage(pagina.PageNumber, shape.Name);
            }
        }
    }
}

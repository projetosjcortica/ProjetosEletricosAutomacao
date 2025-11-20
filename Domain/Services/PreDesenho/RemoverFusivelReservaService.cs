using Domain.Agreggates;
using Domain.Infrastructure;
using Domain.Value_Objects;

namespace Domain.Services.PreDesenho
{
    public class RemoverFusivelReservaService : PreDesenhoServiceBase
    {
        public override void Execute(Page pagina, ICorelDraw corelDraw)
        {
            var deleteAcionamentosReserva = new List<Shape>
            {
                new Shape("fusivel_grupo_1", ""),
                new Shape("fusivel_grupo_2", ""),
                new Shape("fusivel_grupo_3", ""),
                new Shape("fusivel_grupo_4", ""),
                new Shape("fusivel_grupo_5", ""),
                new Shape("fusivel_grupo_6", ""),
                new Shape("fusivel_grupo_7", ""),
                new Shape("fusivel_grupo_8", ""),
                new Shape("fusivel_grupo_9", ""),
                new Shape("fusivel_grupo_10", ""),
            };

            foreach (var shape in deleteAcionamentosReserva)
            {
                corelDraw.DeleteShapeOnPage(pagina.PageNumber, shape.Name);
            }
        }
    }
}

using Domain.Agreggates;
using Domain.Value_Objects;
using Domain.Value_Objects.Common;

namespace Domain.Services.ProjectServices
{
    public class ReversaoService : ProjectServiceBase
    {
        public override void Execute(Project project)
        {
            var reversaoPaginas = project.Paginas
                   .Where(pagina => pagina.IsReversaoPage())
                   .ToList();

            for (int i = 0; i < reversaoPaginas.Count; i++)
            {
                var pagina = reversaoPaginas[i];

                var contatorA = Contator.CreateContatorReversaoA(pagina.GetNomenclatura());
                var contatorB = Contator.CreateContatorReversaoB(pagina.GetNomenclatura());
                var shapeContator1A = new Shape("contator_1a", $"{contatorA.Value}");
                var shapeContator1B = new Shape("contator_1b", $"{contatorB.Value}");
                pagina.AddShape(shapeContator1A);
                pagina.AddShape(shapeContator1B);
                pagina.AddShape(new Shape("disjuntor_1", new Disjuntor(pagina.GetNomenclatura().Replace("FR-","")).Value));

                var anilhaReversao = Anilha.CreateAnilhaReversao(pagina.GetNomenclatura());
                var shapeAnilhaReversao = new Shape("anilha_reversao", anilhaReversao.Value);
                pagina.AddShape(shapeAnilhaReversao);

                if (i % 2 == 0 && i + 1 < reversaoPaginas.Count)
                {
                    var vai = new Shape("vai_reversao", $"(Vai p/ fl. {pagina.PageNumber + 1})");
                    pagina.AddShape(vai);
                }
                else if (i % 2 == 1)
                {
                    var vem = new Shape("vem_reversao", $"(Vem p/ fl. {pagina.PageNumber - 1})");
                    pagina.AddShape(vem);
                }
            }
        }
    }
}

using Domain.Agreggates;
using Domain.Infrastructure;
using Domain.Value_Objects;

namespace Domain.Services.ServicosPosProcessamento
{
    public class RemoverFileiraDeBorneComando : ServicoPosProcessamentoBase
    {
        public override void Execute(ProjetoComposto project, ICorelDraw corelDraw)
        {
            foreach(var projeto in project.Projetos)
            {
                var paginasComando = projeto.Paginas.Where(p => p.IsComandoPage()).ToList();

                DeleteFileiras0V(projeto, paginasComando, corelDraw);
                DeleteFileiras24V(projeto, paginasComando, corelDraw);
            }
        }
        private void DeleteFileiras0V(Project projeto, List<Page> paginasComando, ICorelDraw corelDraw)
        {
            paginasComando.ForEach(p =>
            {
                var deleteFileiras = new List<Shape>
                    {
                        new Shape("fileira_1_0v", ""),
                        new Shape("fileira_2_0v", ""),
                        new Shape("fileira_3_0v", ""),
                        new Shape("fileira_4_0v", ""),
                        new Shape("fileira_5_0v", ""),
                    };

                var shapesParaDeletar = deleteFileiras
                    .Skip(projeto.GetQuantidadeFileiras0V())
                    .ToList();

                foreach (var shape in shapesParaDeletar)
                {
                    corelDraw.DeleteShapeOnPage(p.PageNumber, shape.Name);
                }

                if (projeto.GetQuantidadeFileiras0V() == 0)
                {
                    corelDraw.DeleteShapeOnPage(p.PageNumber, "borne_comando_0v");
                    corelDraw.DeleteShapeOnPage(p.PageNumber, "painel_borne_comando_0v");
                }
            });
        }


        private void DeleteFileiras24V(Project projeto, List<Page> paginasComando, ICorelDraw corelDraw)
        {
            paginasComando.ForEach(p =>
            {
                var deleteFileiras = new List<Shape>
                    {
                        new Shape("fileira_1_24v", ""),
                        new Shape("fileira_2_24v", ""),
                        new Shape("fileira_3_24v", ""),
                        new Shape("fileira_4_24v", ""),
                        new Shape("fileira_5_24v", ""),
                    };

                var shapesParaDeletar = deleteFileiras
                    .Skip(projeto.GetQuantidadeFileiras24V())
                    .ToList();

                foreach (var shape in shapesParaDeletar)
                {
                    corelDraw.DeleteShapeOnPage(p.PageNumber, shape.Name);
                }

                if (projeto.GetQuantidadeFileiras24V() == 0)
                {
                    corelDraw.DeleteShapeOnPage(p.PageNumber, "borne_comando_24v");
                    corelDraw.DeleteShapeOnPage(p.PageNumber, "painel_borne_comando_24v");
                }
            });
        }

    }
}

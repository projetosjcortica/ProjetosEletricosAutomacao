using Domain.Agreggates;
using Domain.Value_Objects;

namespace Domain.Services.ProjectServices
{
    public class AtuadorService : ProjectServiceBase
    {
        public override void Execute(Project project)
        {
            var paginasAtuadoresEFontes = project.Paginas.Where(p => p.IsFonteAtuadorPage() || p.IsAtuadorPage()).ToList();
            var indexFonte = 1;
            foreach (var pagina in paginasAtuadoresEFontes)
            {
                var anilhaPositivaFonteAtuador = Anilha.CreateAnilhaFonteAtuadorPositiva(pagina.Panel, indexFonte);
                var anilhaNegativaFonteAtuador = Anilha.CreateAnilhaFonteAtuadorNegativa(pagina.Panel, indexFonte);

                pagina.AddShape(new Shape("anilha_atuador_positivo", anilhaPositivaFonteAtuador.Value));
                pagina.AddShape(new Shape("anilha_atuador_negativo", anilhaNegativaFonteAtuador.Value));
                pagina.AddShape(new Shape("fonte", pagina.GetNomenclatura()));
                pagina.AddShape(new Shape("anilha_atuador_positivo_fdc_1", Anilha.CreateAnilhaFonteAtuadorPositiva(pagina.Panel, 1).Value));
                pagina.AddShape(new Shape("anilha_atuador_negativo_fdc_1", Anilha.CreateAnilhaFonteAtuadorNegativa(pagina.Panel, 1).Value));

                if (pagina.IsFonteAtuadorPage())
                {
                    var painel = pagina.Panel.Replace("CCM-","");
                    var fonte = pagina.GetNomenclatura().Replace("-","");
                    indexFonte++;
                    pagina.AddShape(new Shape("disjuntor_fonte", "DJ-" + pagina.GetNomenclatura()));
                    pagina.AddShape(new Shape("anilha_disjuntor", $"{painel}-DJ-{fonte}"));
                    pagina.AddShape(new Shape("anilha_disjuntor_1", $"{painel}-DJ-{fonte}.1"));
                }
            }
        }
    }
}
 
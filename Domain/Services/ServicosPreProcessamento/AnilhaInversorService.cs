using Domain.Agreggates;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.ProjectServices
{
    public class AnilhaInversorService : ProjectServiceBase
    {
        public override void Execute(Project project)
        {
            var paginasDeInversores = project.Paginas.Where(p => p.IsInversorPage()).ToList();
            var indexInversor = 1;
            foreach (var pagina in paginasDeInversores)
            {
                var anilhaR = Anilha.CreateAnilhaRInversor(pagina.Panel, indexInversor);
                var anilhaS = Anilha.CreateAnilhaSInversor(pagina.Panel, indexInversor);
                var anilhaT = Anilha.CreateAnilhaTInversor(pagina.Panel, indexInversor);
                var anilhaC = Anilha.CreateAnilhaCInversor(pagina.Panel, indexInversor);
                var anilhaC1 = Anilha.CreateAnilhaCInversor(pagina.Panel, indexInversor).Value + ".1";
                var anilhaU = Anilha.CreateAnilhaUInversor(pagina.Panel, indexInversor);
                var anilhaV = Anilha.CreateAnilhaVInversor(pagina.Panel, indexInversor);
                var anilhaW = Anilha.CreateAnilhaWInversor(pagina.Panel, indexInversor);

                pagina.AddShape(new Shape("anilha_inversor_r", anilhaR.Value));
                pagina.AddShape(new Shape("anilha_inversor_s", anilhaS.Value));
                pagina.AddShape(new Shape("anilha_inversor_t", anilhaT.Value));
                pagina.AddShape(new Shape("anilha_inversor_c", anilhaC.Value));
                pagina.AddShape(new Shape("anilha_inversor_c_1", anilhaC1));
                pagina.AddShape(new Shape("anilha_inversor_u", anilhaU.Value));
                pagina.AddShape(new Shape("anilha_inversor_v", anilhaV.Value));
                pagina.AddShape(new Shape("anilha_inversor_w", anilhaW.Value));

                indexInversor++;
            }
        }
    }
}

using Domain.Agreggates;
using Domain.Services.ProjectServices;
using Domain.Value_Objects;
using System.Text.RegularExpressions;

namespace Domain.Services.ServicosPreProcessamento
{
    public class RoscasDosadoraService : ProjectServiceBase
    {
        public override void Execute(Project project)
        {
            var paginasGrupoInversoRoscasDosadoras = project.Paginas.Where(p => p.GetNomenclatura().Contains("GRD") || p.GetNomenclatura().Contains("GRE"));

            foreach (var pagina in paginasGrupoInversoRoscasDosadoras)
            {
                List<Page> roscasDeDosagem = [];
                if(pagina.GetNomenclatura().Contains("GRD"))
                    roscasDeDosagem = EncontrarRoscasDeDosagemDoGRD(project, pagina.GetNomenclatura());
                if (pagina.GetNomenclatura().Contains("GRE"))
                    roscasDeDosagem = EncontrarRoscasDeDosagemDoGRE(project, pagina.GetNomenclatura());  
                ProcessarGrupoDePaginasDeRoscasDeDosagem(pagina, roscasDeDosagem);
            }

        }

        private void ProcessarGrupoDePaginasDeRoscasDeDosagem(Page pagina, List<Page> roscasDeDosagem)
        {
            if (roscasDeDosagem.Count == 0) return;

            List<Page> todasPaginas = ObterTodasPaginas(pagina, roscasDeDosagem);
            var numeroTodasPaginas = todasPaginas.Select(p => p.PageNumber).ToList();

            var gruposDeVemEVai = AgruparVemEVai(numeroTodasPaginas);
            for (int i = 0; i < todasPaginas.Count; i++)
            {
                var paginaAtual = todasPaginas[i];
                var (vem, vai) = gruposDeVemEVai[i];
                AdicionarVemEVaiNaPagina(paginaAtual, vem, vai);
                AdicionarAnilhasNaPagina(pagina, paginaAtual);
            }
        }

        private static List<Page> ObterTodasPaginas(Page? pagina, List<Page> roscasDeDosagem)
        {
            var todasPaginas = new List<Page> { pagina };
            todasPaginas.AddRange(roscasDeDosagem);
            return todasPaginas;
        }

        private static void AdicionarAnilhasNaPagina(Page pagina, Page paginaAtual)
        {
            if (paginaAtual.GetNomenclatura().Contains("GRD") || paginaAtual.GetNomenclatura().Contains("GRE"))
                return;

            var anilhaU = new Shape("anilha_u", pagina.Shapes.First(s => s.Name == "anilha_inversor_u").Text);
            var anilhaV = new Shape("anilha_v", pagina.Shapes.First(s => s.Name == "anilha_inversor_v").Text);
            var anilhaW = new Shape("anilha_w", pagina.Shapes.First(s => s.Name == "anilha_inversor_w").Text);


            var indexInversor = pagina.Shapes.First(s => s.Name == "anilha_inversor_u").Text.Split("INV").Last();
            var u = new Shape("u", Anilha.CreateAnilhaUInversorSemQuebra(pagina.Panel, int.Parse(indexInversor)).Value + " (    )");
            var v = new Shape("v", Anilha.CreateAnilhaVInversorSemQuebra(pagina.Panel, int.Parse(indexInversor)).Value + " (    )");
            var w = new Shape("w", Anilha.CreateAnilhaWInversorSemQuebra(pagina.Panel, int.Parse(indexInversor)).Value + " (    )");


            paginaAtual.AddShape(anilhaU);
            paginaAtual.AddShape(anilhaV);
            paginaAtual.AddShape(anilhaW);
            paginaAtual.AddShape(u);
            paginaAtual.AddShape(v);
            paginaAtual.AddShape(w);
        }

        private static void AdicionarVemEVaiNaPagina(Page pagina, int vem, int vai)
        {
            var vemUVW = new Shape("vem_uvw", $"(Vem da fl. {vem.ToString().PadLeft(2, '0')})");
            var vaiUVW = new Shape("vai_uvw", $"(Vai para fl. {vai.ToString().PadLeft(2, '0')})");
            pagina.AddShape(vemUVW);
            pagina.AddShape(vaiUVW);
        }

        private List<Page> EncontrarRoscasDeDosagemDoGRD(Project projeto, string nomenclaturaGRD)
        {
            var match = Regex.Match(nomenclaturaGRD, @"GRD\((\d{1,2})-(\d{1,2})\)");

            if (!match.Success)
            {
                Console.WriteLine($"Padrão 'GRD-XX-XX' não encontrado na nomenclatura {nomenclaturaGRD}");
                return new List<Page>();
            }

            int primeiraRocasDeDosagem = int.Parse(match.Groups[1].Value);
            int ultimaRoscaDeDosagem = int.Parse(match.Groups[2].Value);
            var paginasRoscasDeDosagem = projeto.Parent.Projetos
                .SelectMany(projeto => projeto.Paginas)
                .Where(p =>
                {
                    var matchPagina = Regex.Match(p.GetNomenclatura(), @"RD-(\d{1,2})");
                    if (!matchPagina.Success)
                        return false;

                    int numero = int.Parse(matchPagina.Groups[1].Value);
                    return numero >= primeiraRocasDeDosagem && numero <= ultimaRoscaDeDosagem;
                })
                .ToList();

            return paginasRoscasDeDosagem;
        }

        private List<Page> EncontrarRoscasDeDosagemDoGRE(Project projeto, string nomenclaturaGRD)
        {
            var match = Regex.Match(nomenclaturaGRD, @"GRE\((\d{1,2})-(\d{1,2})\)");

            if (!match.Success)
            {
                Console.WriteLine($"Padrão 'GRE-XX-XX' não encontrado na nomenclatura {nomenclaturaGRD}");
                return new List<Page>();
            }

            int primeiraRocasDeDosagem = int.Parse(match.Groups[1].Value);
            int ultimaRoscaDeDosagem = int.Parse(match.Groups[2].Value);
            var paginasRoscasDeDosagem = projeto.Parent.Projetos
                .SelectMany(projeto => projeto.Paginas)
                .Where(p =>
                {
                    var matchPagina = Regex.Match(p.GetNomenclatura(), @"RD-EX-(\d{1,2})");
                    if (!matchPagina.Success)
                        return false;

                    int numero = int.Parse(matchPagina.Groups[1].Value);
                    return numero >= primeiraRocasDeDosagem && numero <= ultimaRoscaDeDosagem;
                })
                .ToList();

            return paginasRoscasDeDosagem;
        }

        private List<(int, int)> AgruparVemEVai(List<int> numeroDePaginas)
        {
            List<(int, int)> resultado = new List<(int, int)>();

            for (int i = 0; i < numeroDePaginas.Count; i++)
            {
                int primeiro = (i == 0) ? 0 : numeroDePaginas[i - 1];
                int segundo = numeroDePaginas[i + 1 < numeroDePaginas.Count ? i + 1 : 0];
                resultado.Add((primeiro, segundo));
            }

            return resultado;
        }
    }
}

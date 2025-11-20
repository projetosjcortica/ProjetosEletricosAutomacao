using Domain.Factories;
using Domain.Value_Objects.Partidas;

namespace Domain.Agreggates
{
    public class Project
    {
        public string Painel { get; set; } 
        public List<Page> Paginas { get; private set; }
        public List<ProjectInfo> ProjectInfos { get; private set; }

        public ProjetoComposto Parent { get; set; }

        public Project(List<Page> paginas, List<ProjectInfo> projectInfo, string painel)
        {
            Paginas = paginas;
            ProjectInfos = projectInfo;
            Painel = painel;
        }
        public Project(List<ProjectInfo> projectInfo)
        {
            Paginas = [];
            ProjectInfos = projectInfo;
        }
        public void AddPage(Page pagina)
        {
            pagina.SetPanel(Painel);
            Paginas.Add(pagina);
        }
        public void SetPainel(string value)
        {
            Painel = value;
        }
        public int GetNumberOfPages()
        {
            return Paginas.Count;
        }

        public Dictionary<string, string> GetProjectInfos()
        {
            var Result = new Dictionary<string, string>();
            foreach(var ProjectInfo in ProjectInfos)
            {
                Result.Add(ProjectInfo.Nome, ProjectInfo.Value);
            }

            return Result;
        }

        public int Paginar(int pageNumber = 1)
        {
            foreach (var pagina in Paginas)
            {
                pagina.SetPageNumber(pageNumber);
                pageNumber++;
            }

            return pageNumber;
        }

        public List<string> PainelList()
        {
            return Paginas.Select(p => p.Panel).Distinct().OrderBy(name => name).ToList();
        }

        public void Processar()
        {
            var handler = ProjectServicesFactory.Create();
            handler.Handle(this);
        }

        public int GetQuantidadeFileiras0V()
        {
            var fileiras = new HashSet<string>();

            foreach (var info in ProjectInfos)
            {
                if (!string.IsNullOrEmpty(info.Value) &&
                    info.Nome.Contains("0v", StringComparison.OrdinalIgnoreCase))
                {
                    var partes = info.Nome.Split('_');
                    var fileira = partes.Length > 1 ? partes[1] : null;

                    if (fileira != null)
                        fileiras.Add(fileira);
                }
            }

            return fileiras.Count;
        }

        public int GetQuantidadeFileiras24V()
        {
            var fileiras = new HashSet<string>();

            foreach (var info in ProjectInfos)
            {
                if (!string.IsNullOrEmpty(info.Value) &&
                    info.Nome.Contains("24v", StringComparison.OrdinalIgnoreCase) &&
                    info.Nome.Contains("borne", StringComparison.OrdinalIgnoreCase))
                {
                    var partes = info.Nome.Split('_');
                    var fileira = partes.Length > 1 ? partes[1] : null;

                    if (fileira != null)
                        fileiras.Add(fileira);
                }
            }

            return fileiras.Count;
        }
    }
}

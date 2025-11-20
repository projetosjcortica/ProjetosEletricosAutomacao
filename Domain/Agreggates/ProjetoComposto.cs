using Domain.Factories;
using Domain.Infrastructure;
using Domain.Services.ServicosPosProcessamento;

namespace Domain.Agreggates
{
    public class ProjetoComposto
    {
        public readonly List<Project> Projetos;
        public readonly ICorelDraw corelDraw;

        public ProjetoComposto(List<Project> projects, ICorelDraw corel)
        {
            Projetos = projects;
            corelDraw = corel;
        }

        public ProjetoComposto(ICorelDraw corel)
        {
            Projetos = [];
            corelDraw = corel;
        }

        public void AddProject(Project project)
        {
            Projetos.Add(project);
        }

        private void Paginar()
        {
            var pageNumber = 1;
            foreach (var project in Projetos)
            {
                pageNumber = project.Paginar(pageNumber);
            }
        }

        public void CriarProjeto()
        {
            var PreDesenhoService = PreDesenhoServicesFactory.Create();
            PreProcessar();
            foreach(var projeto in Projetos)
            {
                foreach(var pagina in projeto.Paginas)
                {
                    if(pagina.PageNumber != 1)
                        corelDraw.CreateNewPageProjetoEletrico();
                    corelDraw.DuplicatePageBetweenFiles(int.Parse(pagina.DescriptionPage.PageReference), pagina.PageNumber);
                    PreDesenhoService.Handle(pagina, corelDraw);
                    corelDraw.InsertDataInPage(pagina);
                    corelDraw.InsertDataInPage(pagina, projeto.GetProjectInfos());
                    corelDraw.UpdateShapesOnPage(pagina);              
                }
            }
            PosProcessar();
        }

        public void PreProcessar()
        {
            Paginar();
            foreach (var project in Projetos)
            {
                project.Processar();
            }
        }

        public void PosProcessar()
        {
            var DeletarBorne = new RemoverBornesComandoService();
            var DeletarVentiladores = new RemoverVentiladorComandoService();
            var DeletarFileiraBorne = new RemoverFileiraDeBorneComando();
            DeletarBorne.Execute(this, corelDraw);
            DeletarVentiladores.Execute(this, corelDraw);
            DeletarFileiraBorne.Execute(this, corelDraw);
        }
    }
}

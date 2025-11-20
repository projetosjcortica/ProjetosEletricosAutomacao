using Application.DTO;
using Domain.Agreggates;
using Domain.Infrastructure;
using Domain.Repositories;

namespace Application.UseCases
{
    public class CreateProjects
    {
        private IExcelRepository _ExcelRepository;
        private ICorelDraw CorelDraw;
        public CreateProjects(IExcelRepository excelRepository, ICorelDraw corelDraw)
        {
            _ExcelRepository = excelRepository;
            CorelDraw = corelDraw;
        }
        public void Execute(List<string> panelsNames)
        {
            CorelDraw.Init();
            var projects = new ProjetoComposto(CorelDraw);
            foreach (var panelName in panelsNames)
            {
                var BasePath = @$"C:\Users\maqui\Documents\Painel {panelName}.xlsx";
                _ExcelRepository.SetPanelName(panelName);
                _ExcelRepository.SetFilePath(BasePath);

                var createProject = new CreateProject(_ExcelRepository, CorelDraw);

                var input = new InputCreateProject(panelName);
                var project = createProject.Execute(input);
                project.Parent = projects;

                projects.AddProject(project);
            }

            projects.CriarProjeto();
        }
    }
}

using Application.DTO;
using Domain.Agreggates;
using Domain.Factories;
using Domain.Infrastructure;
using Domain.Repositories;

namespace Application.UseCases
{
    public class CreateProject
    {
        private IExcelRepository _ExcelRepository;
        private ICorelDraw CorelDraw;

        public CreateProject(IExcelRepository excelRepository, ICorelDraw corelDraw)
        {
            _ExcelRepository = excelRepository;
            CorelDraw = corelDraw;
        }
        public Project Execute(InputCreateProject input)
        {
            var listPages = new List<Page>();
            var DescriptionPages = _ExcelRepository.GetDescriptionPages();

            for(int i = 0; i < DescriptionPages.Count; i++)
            {
                var DescriptionPage = DescriptionPages[i];
                
                var PageData = _ExcelRepository.GetPageDataByNomenclatura(DescriptionPage.Nomenclatura.Value);


                var Page = FactoryPage.CreatePage(DescriptionPage, PageData);
                Page.SetPanel(_ExcelRepository.GetPanelName());
                listPages.Add(Page);
            }

            var ProjectsInfo = _ExcelRepository.GetInformacoesEspeciais();

            var Project = new Project(listPages, ProjectsInfo, input.PanelName);

            return Project;
        }

        public void PrintDictionary(Dictionary<string, string> dictionary)
        {
            if (dictionary != null && dictionary.Count > 0)
            {
                foreach (var kvp in dictionary)
                {
                    Console.WriteLine($"Chave: {kvp.Key}, Valor: {kvp.Value}");
                }
            }
            else
            {
                Console.WriteLine("O dicionário está vazio ou é nulo.");
            }
        }
    }
}
